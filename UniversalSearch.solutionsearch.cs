using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.CSharp;
using ICSharpCode.Decompiler.CSharp.ProjectDecompiler;
using ICSharpCode.Decompiler.Metadata;
using Microsoft.Crm.Sdk.Messages;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using xrmtb.XrmToolBox.Controls;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Args;

namespace MikeFactorial.XTB.Plugins.UniversalSearch
{
    public partial class UniversalSearch
    {
        private void LoadSolution(object selectedSolution)
        {
            this.tabControl1.TabPages.Clear();
            WorkAsync(new WorkAsyncInfo
            {
                IsCancelable = true,
                Message = $"Exporting {((ListDisplayItem)selectedSolution).ToString()}...",
                AsyncArgument = selectedSolution,
                Work = (worker, args) =>
                {
                    long recordCount = 0;
                    int fileCount = 0;
                    int errors = 0;
                    string solutionName = ((ListDisplayItem)selectedSolution).Name;
                    string solutionZipFileName = solutionName + ".zip";
                    if (alwaysGetLatestSolutionCheckBox.Checked || !File.Exists(solutionZipFileName) || !Directory.Exists(solutionName))
                    {
                        // Export a solution
                        ExportSolutionRequest exportSolutionRequest = new ExportSolutionRequest();
                        exportSolutionRequest.Managed = false;
                        exportSolutionRequest.SolutionName = solutionName;

                        ExportSolutionResponse exportSolutionResponse =
                           (ExportSolutionResponse)this.Service.Execute(exportSolutionRequest);

                        byte[] exportXml = exportSolutionResponse.ExportSolutionFile;
                        if (worker.CancellationPending)
                        {
                            return;
                        }

                        if (File.Exists(solutionZipFileName))
                        {
                            File.Delete(solutionZipFileName);
                        }
                        File.WriteAllBytes(solutionZipFileName, exportXml);
                        if (Directory.Exists(solutionName))
                        {
                            Directory.Delete(solutionName, true);
                        }
                        if (worker.CancellationPending)
                        {
                            return;
                        }
                        ZipFile.ExtractToDirectory(solutionZipFileName, solutionName);
                        if (worker.CancellationPending)
                        {
                            return;
                        }
                    }
                    var files = Directory.GetFiles(solutionName, "*.*", SearchOption.AllDirectories);
                    for (int i = 0; i < files.Length; i++)
                    {
                        try
                        {
                            string textToSearch = searchTextBox.Text;
                            double percentage = (double)(i + 1) / (double)files.Length;
                            percentage *= (double)100;
                            Thread.Sleep(500);
                            worker.ReportProgress(Convert.ToInt32(percentage), $"Searching {files[i]}.");

                            if (worker.CancellationPending)
                            {
                                return;
                            }
                            List<SolutionSearchResult> results = new List<SolutionSearchResult>();
                            if (files[i].EndsWith(".dll"))
                            {
                                var dllUnpackPath = files[i].Replace(".dll", "");
                                if (Directory.Exists(dllUnpackPath))
                                {
                                    Directory.Delete(dllUnpackPath, true);
                                }

                                var projectDir = Directory.CreateDirectory(dllUnpackPath);

                                string projectFileName = Path.Combine(projectDir.FullName, Path.GetFileNameWithoutExtension(files[i]) + ".csproj");
                                using (var module = new PEFile(files[i]))
                                {
                                    var resolver = new UniversalAssemblyResolver(files[i], false, null);
                                    var decompiler = new WholeProjectDecompiler(new DecompilerSettings(LanguageVersion.Latest)
                                    {
                                        ThrowOnAssemblyResolveErrors = false,
                                        RemoveDeadCode = true,
                                        RemoveDeadStores = true,
                                        UseSdkStyleProjectFormat = WholeProjectDecompiler.CanUseSdkStyleProjectFormat(module),
                                        UseNestedDirectoriesForNamespaces = false,
                                    }, resolver, resolver, null);
                                    using (var projectFileWriter = new StreamWriter(File.OpenWrite(projectFileName)))
                                    {
                                        decompiler.DecompileProject(module, Path.GetDirectoryName(projectFileName), projectFileWriter);
                                    }
                                }
                                var sourceCodeFiles = Directory.GetFiles(projectDir.FullName, "*.*", SearchOption.AllDirectories);
                                foreach (var sourceCodeFile in sourceCodeFiles)
                                {
                                    searchSolutionObject(sourceCodeFile, textToSearch, ref results);
                                }

                            }
                            if (files[i].EndsWith(".msapp"))
                            {
                                var msappUnpackPath = files[i].Replace(".msapp", "");
                                if (Directory.Exists(msappUnpackPath))
                                {
                                    Directory.Delete(msappUnpackPath, true);
                                }

                                var canvasDir = Directory.CreateDirectory(msappUnpackPath);

                                ZipFile.ExtractToDirectory(files[i], msappUnpackPath);

                                var appFiles = Directory.GetFiles(msappUnpackPath, "*.*", SearchOption.AllDirectories);
                                foreach (var appFile in appFiles)
                                {
                                    searchSolutionObject(appFile, textToSearch, ref results);
                                }
                            }
                            else
                            {
                                searchSolutionObject(files[i], textToSearch, ref results);
                            }
                            recordCount += results.Count;
                            AddSolutionResultTab(Path.GetFileName(files[i]), results);
                            fileCount++;
                        }
                        catch (Exception e)
                        {
                            errors++;
                            try
                            {
                                this.LogError(e.ToString());
                            }
                            catch { }
                            worker.ReportProgress(0, $"Search Completed with Errors. Error Message: " + e.Message);
                        }
                    }
                    if (recordCount == 0)
                    {
                        worker.ReportProgress(0, $"Search Complete. No files were found. Make sure you've selected the correct solution to search and that you are using * for wildcard searches otherwise the exact text will be matched.");
                    }
                    else
                    {
                        worker.ReportProgress(0, $"Search Complete. Found {recordCount} instances in {fileCount} files with {errors} error(s).");
                    }

                },
                ProgressChanged = (args) =>
                {
                    SetWorkingMessage(args.UserState?.ToString());
                    SendMessageToStatusBar(this, new StatusBarMessageEventArgs(args.ProgressPercentage, args.UserState.ToString().Replace("\r\n", " ")));
                },
                PostWorkCallBack = (args) =>
                {
                    this.searchLocationList.Enabled = true;
                    this.btnFind.Text = "Search";
                    this.openInExcelToolStripMenuItem.Enabled = (tabControl1.TabPages.Count > 0);
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.Message, "Uh oh.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            });
        }
        private void findInSolution()
        {
            if (btnFind.Text.StartsWith("Search"))
            {
                if (this.SolutionDropDownComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Please select a solution to search before continuing.", "No Solution Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (string.IsNullOrEmpty(searchTextBox.Text))
                {
                    MessageBox.Show("Please enter your search criteria in the search box. Use asterisks * to perform a wildcard search", "No Search Criteria Entered", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    this.btnFind.Text = "Cancel";
                    this.searchLocationList.Enabled = false;
                    this.openInExcelToolStripMenuItem.Enabled = false;
                    LoadSolution(this.SolutionDropDownComboBox.SelectedItem);
                }
            }
            else
            {
                CancelWorker(); // PluginBaseControl method that calls the Background Workers CancelAsync method.
                this.btnFind.Text = "Search";
                this.searchLocationList.Enabled = true;
                this.openInExcelToolStripMenuItem.Enabled = (tabControl1.TabPages.Count > 0);
            }
        }


        private void searchSolutionObject(string filePath, string searchText, ref List<SolutionSearchResult> results)
        {
            //Format xml documents first
            if (filePath.EndsWith(".xml"))
            {
                try
                {
                    // Load the XML file
                    XDocument doc = XDocument.Load(filePath);
                    // Format the XML with indents and line breaks
                    string formattedXml = doc.ToString();

                    // Write the formatted XML to a new file
                    File.WriteAllText(filePath, formattedXml);
                }
                catch
                {
                    //Fine to fail here. We'll just search the unformatted version of the file
                }
            }
            else if (filePath.EndsWith(".json"))
            {
                try
                {
                    // Read the JSON file into a string
                    string json = File.ReadAllText(filePath);
                    // Parse the string into a JObject
                    JObject parsedJson = JObject.Parse(json);
                    // Format the JSON with indents and line breaks
                    string formattedJson = parsedJson.ToString(Newtonsoft.Json.Formatting.Indented);
                    // Write the formatted JSON to a new file
                    File.WriteAllText(filePath, formattedJson);
                }
                catch
                {
                    //Fine to fail here. We'll just search the unformatted version of the file
                }
            }
            var regEx = "^" + Regex.Escape(searchText).Replace("\\*", ".*") + "$";
            var lines = File.ReadLines(filePath).ToList();
            for (int i = 0; i < lines.Count; i++)
            {
                if ((!solutionMatchCaseCheckBox.Checked && Regex.IsMatch(lines[i], regEx, RegexOptions.IgnoreCase)) || (solutionMatchCaseCheckBox.Checked && Regex.IsMatch(lines[i], regEx)))
                {
                    results.Add(new SolutionSearchResult() { FileLink = filePath, LineNumber = (i + 1), FilePath = filePath, Value = lines[i] });
                }
            }
        }

    }
}
