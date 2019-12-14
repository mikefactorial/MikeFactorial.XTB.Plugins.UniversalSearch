using MikeFactorial.XTB.Plugins;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Reflection;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace MikeFactorial.XTB.Plugins.UniversalSearch
{
    // Do not forget to update version number and author (company attribute) in AssemblyInfo.cs class
    // To generate Base64 string for Images below, you can use https://www.base64-image.de/
    [Export(typeof(IXrmToolBoxPlugin)),
        ExportMetadata("Name", "Universal Search"),
        ExportMetadata("Description", "This tool will allow you to search across all records in Dynamics 365 for a specific value or wildcard values."),
        // Please specify the base64 content of a 32x32 pixels image
        ExportMetadata("SmallImageBase64", "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAABGdBTUEAALGPC/xhBQAAAAlwSFlzAAAOwgAADsIBFShKgAAAABh0RVh0U29mdHdhcmUAcGFpbnQubmV0IDQuMS40E0BoxAAABGpJREFUWEftVltIY2cQNslJoia6JvFWo3jFGq3XeqvK+lQrKLRalFKo9KEvRaGUQPVBsT4UitCl0AfbBVdFob76UJQ+6JNudeP9AoKCiruo1RgVDMrq9Jt/YxrrMZu4+1Y/GGbOf84/M//MnJk/4B738AdlZWUGs9lcFRoa+pNOpxsOCQmZCwsLW2QZa48SEhI+zszMNLg+f3tob2+XIiMjrWq1+m+FQnGJJZIjvCNJknYMBoO1vLxcwtqbIzY2NlOj0UxClDXqSfn5+dTZ2UlVVVUUGBhoi46OtmD97sBJMnCifYiyBqUQIxk/+ETIERERtLm5Saenp+R0OikrK4sQsZPc3NyHeO8/cPIUDjnEG4Y9SaFSC47v6ejoiC4vL+ni4oJKS0vFOnQ8RyTiIfsHvV7/JxhJeiMFILcsMymVSiooKKDm5mbq7e2lnp4eamlpocLCQmpoaKCpqSlqa2sT313tCQ4OfgruO1JSUj4FE5vNn7WT2hAtZFQ5dXV10fj4OE1MTAhj09PTNDMzI6ivr49SU1PdhtWGdyj28+8RJYni4uK+xNrrwdWLAlqA+EqR6yTJyck0MjIijE9OTtLs7CzNzc3doLGxMUpKSiKlNpjebf+D8n93kLGkhrRa7dO6ujoV2/CK+Ph4C8J3BvHfk6jVItxsnE8tZ9iThoaGKCgoiJSaIIr86CuhA8XsRBRiIHsHqrkOzG2cqbKy0h12OYNyVF9ff00HE6L4Bbh3wIEfwMQGLiTOe3d3t3DAZrPJGpOjgYEBSkxMJJVK5XYgPDy8E9w78O//AkZ5eXnU399Pe3t7IufswG15l6OVlRVyOBw0ODgo/hrWiXb9WBjxBuTuR67k/f19Wltbo8PDQ1pdXfUp91e0uLhIGxsbdHJyIvYeHBwIJ4xGY5fLzO1A0/gWA4a2t7eJwY1lfn6eFhYWRIfjiGxtbdH6+jotLS0JY+wor/E77oTsBL/jvQxe40aF9H7nMnM74MBDHjjcSoeHh0UYOQp8Gk+wcja+vLzsNnSF4+Nj9x7+LTFFeVBdWiyWIpeZ25Genm5E8dkhigLiQmpsbBRR4F6/s7Mj0mO3268Rr+3u7opIsFNWq5XS0tIIg0zkH/xFSUlJiDDyOiBUv4GJjQrpVa+vra0Vjei/+Wbi4ryi0dFRqqmpEXs8CXeHX8F9Q05OTgRPMklvoKRvetDVdGLW4+JBHR0dIsTn5+d0dnbmJi601tZWrnTxrTYq0W0ckXRmZ2cnCOW+wmSK+Dost+LivUfPSJfy/rXTsJHq6mpqamoS6eH5z2ue33D/F1yheBkVFdUI2T9w30ZPeKIKfnBNsb8Ex574NAPkgAqWMJZ/RlG+xOMN5RqTmR7kfHhjnQknP8fv/PiNr2bsPRRVoyZ28HjTkCvUnoTJZ4+JiamHrAS9HVRUVBhNJpMVyv/CZHN4XjhYhoPHuHjY+ELq8+92F3BE0NWMGRkZluLi4pqioqJa9Ap079TwO+f6Hv9jBAT8A9HMnmAOGeCPAAAAAElFTkSuQmCC"),
        // Please specify the base64 content of a 80x80 pixels image
        ExportMetadata("BigImageBase64", "iVBORw0KGgoAAAANSUhEUgAAAFAAAABQCAYAAACOEfKtAAAABGdBTUEAALGPC/xhBQAAAAlwSFlzAAAOwgAADsIBFShKgAAAABh0RVh0U29mdHdhcmUAcGFpbnQubmV0IDQuMS40E0BoxAAADNpJREFUeF7tW2lQVFcWtqFBVmVfVFAEFJWAqLghYtTAGEGHJJKpyAgyhlEjVVRcSouacUbNUDqWlo5GJ4lLmZiZcQTyw1iWSXQ0cRf3XeOemLjvgtuZ8125bS+vu0H7Najvq/rq3Xf7Lfd+79xzzr3vdSMNGjRo0KBBgwYNGjRoeHWRl5fnN2rUqISMjIz8pk2b5kdGRo578803K8Du3bvP9PX1zU9NTc0fNmxYwuDBg/1WrFjhWnPqq4nJkyf79O/fv19ycvJiPz+/nUFBQY9dXFxIp9MJ8iEmZeN9Pp78/f0PdejQYfGQIUNy+VohuOYrgffee++1zp07L2IruyIF4+pnIs7FNZo0aXI3KipqMVtrT65/OTF06NDUNm3abHB3d3/8PKJZI67p6ur6KDQ0dEPbtm27cN3LgWnTpoUHBATMdHNzU0U4JfJ9HsfFxW3s1KlTOO+/mICTj4+Pz2aLO8O7ih11FGusjzw8PEiv14t9kAPP/ejo6EI+xoX54qCoqKhJRETEYvgn3lWVbNk0fPhw2r59O509e5Z27txJI0eOJH5w4ne0ITY29n/p6ekBvN/w0bdv31B26McdNVx1rnrFehDWtmjRIrp37x49evSIJB4/fkw8AsTvOA5t8fT0PDRw4MDWvN9wwUEiiCPiES5adPZZ6OYXSjHjviBXryaKvw8YMECI99NPP9HVq1fp0qVLQkCICRE54pscHxwc/IDzyNe53PDAqUmQj4/PUUdZHhiQ8jYlLTpJPnHdLH7DfT7//HMh1PXr16m6uloQuH//Pl25coUqKirEccbncb5ZVVxcnMrlhoNJkyaFhoWFHXGkeJLuQS0U63Gvb775Rgj24MEDqqqqEmWJhw8f0saNGy0EBFnESzzT8edy/SMtLU3PuVe5GuLZIu43ffp0YYFKQP3ChQsVBURd8+bND/Asxp336xecqkx0dLRFB407br4vyT6Xbt26VSPZE0A4WB98Y0JCgsU5krgeB7uFXNYx6wecqEZ7e3s/5KJZA+tmjegM0g5+GMQzFpo6dSotXbqUVq5cSWVlZbRs2TIqLS2l/Px8Yl9LXl5eBkE5stK1a9eEcCDKN27coHfffdfiPuZElM7IyBjI5foBP8EtvDFtGHcs+I3h5NY0xLRegRCBhz998MEHtHr1atq8eTP98MMPtGnTJkXK37799lsaP348tWrVynCNDz/8kObOnSvqmzVrZhDYHnlOjkTf+eB8b4hlI1m8/sOpy/Kr1O6jdeTRLNbs96fkdIc4GgpHb0s0iLpt2zbasWOHSJIlsY96WCsERFskle73hDoKyxxNbgHhhjoc361bt79z2XlYsmSJB0exi1w0atwTerVKoMQFRyiyYKbV/A3D8KuvvlIUDNyyZYsQaPfu3bRnzx67xDkYyvZ8sXtwJHX5zw1qXfQpj5Snx/r7+98dO3as85bEOIEdaaux3rHJ5NLYy6Ie57z11lvC6sxFA2FtsC4lkexx165dwqIxJza/LyzPKyqR2pduoK7l9yl5xW0KG1RMLh7e4vcaK/yIy84BZ/SWvs8O0UjMDKwNV8xla2txtjhx4kQLSwzun0ddy+4I8Qwsq6bEj/cZHjRb4bk5c+Y05rK6yMnJScQEnot1Yp8+fayK96xWZ408XTPxhToXVwrq+3ue1ZyuEa+KYsZ/SY1DWhqOgeWmpKT04rK6SExM/JttR21JTlrFrEFJvMrKSkURnod4IElJSWbt0JF3TGcW7x7F/WW1ENX090ZIo1bzVl1w8DjLG4ubWyOG0+zZs50mnuSqVavE+qBJe/jBR43+Bwe610zra+jr63te1dlJdnZ2C05iFRJn6+zRo4cIDubiwecpddyRzMvLs2iPveUxDnItuawOMjMzs+oyfHHsJ598YiEeBFXqsKO5du1aSyu0QbQ3Li6umMvqICQkpLguAsbGxipan6ODhi0ieCm1TYnoG6cz67isDtLS0nbyRvHmSiwsLLSIvEiSlTqqFmfNmmUSke3Rx8dnPW/VAU+ZaiUgGswOmZYvX24iHogZhlJH1eJ3332H9b9ai8jz+5uqffXAuZJVAdHAsLAwys3NFdO0n3/+WTHvc0SyXFdevHiR1q1bJxYtYmJibE75sBDBkdjxAq5fv17PEfgoF01uiMZ07NiROIunkydPitVhAEtK5gI6K3iY8+bNm6JNWO7CuxO8cGJ3pCgkz0jUEbCiosIPT4+LBgYEBNCSJUto69atBpH27dsnGnr58mUT8UBn+z9J+bLp119/NbQFDxO5IgKdcZ+wSqSKgBw53divneCiuFFgYKAQ68KFCyYigVgN/uWXXyzqIbRSB9WmFBALDsbtOXjwoLBInl0ZBIRRqCIg4O7uLnwg/N2MGTOEpeFNGEQ8ffq0eLGNxqIey+rnz58XKYscys4UEL4WDxhtkm4FvvD48eN05MgR+vHHH4V4wJo1awxBpkWLFuoJyBc3CIgX2hDKFiAihg1845kzZ8QTR6fUCCS45t69e+no0aPiweGeeLB4xWkPcC1SQM51T02ePFmdT0G6du36NW/EjTBZv337dk0TLIGX2+iQHC6Y90JQ1ONFEKI0rODQoUPiOAggqSQQaHwMzsEDgUVBMLwbhqXh+nhI5ve1BhgBMgfZLw4i6uWBLVu2NMxEsEVaYO0JQyTZCUm87DYHOiBdAR4IhEAAOnfunJgvY9ke1oQ6vDDCdfEOWH59YI7a3heAsLh+48aNDX3q0KHDl6KzaqB37949jEM/1tCmTJkihqm5kLAG+B9YCIghLL8csAecC/GkABhitT0X7cC9jh07JgjfbH4uro8HgtyQE2dDfyBgamqqenPhCRMmtOBI/ICLhptC0PT0dLHeB8FgQeiEPf9oC0pWhA4/K9AWiIZ8EMMd7qCkpETMloz7AkscM2ZMFJfVQ2Rk5B7emNwYxHtdCInpG3zTqVOnhBNHQo0hZ8sPmQMdxnBG1AQhKIZsbYFjYXUQDKMDbYE1btiwgdgI8JGRIWgYk6eqFzjJVvdrBU48xyjdXBIWiQbiBTmWsjD8EBlBBA34NnQKfgni3rlzRwiMDsNKZKCRPk4S+/gNx8DCcc7du3eFSPLLLBl9IRbut3//fvGB0aRJk8RsCa8irLUd9e3bt1/GZXWRlZUVpdfrTYaxNaJR8JOfffaZiJoHDhwQ1nn48GGRi0lhUZaUdaD0Y1IQW8eCuC6uD+HKy8vJ29tbtMHWA5eEuCNGjOjLZfURHx9fyRvFhiiRozd9//33Iqk2JlIMmbZAYKQf6LwkBJc0rsdxOF6mNriO8XUxTWNrUmyLNfLwPcZb56Bfv36/UZqI22JGRoaIrOaddTRxj3feeadWVieJvnBe+0cuOw88K9nFG4vG4GsEr9YdLerRIc6xiF0AzZ8/X6QXJ06cEEMPybS0LKWk2ZzSYnGOdAkIXtOmTcM0rE7igRyNz/J5zv3Uja2wj/wG2UBuePPf/YniZ+8gnf7JR95KhF8sKCgwzJsB84Bhj/I4BKPRo0eLa5oLZ+sFkiTO4fx2LJedj3bt2q3izZOG6N0oJP0P1OXf16lreTVFjfqY9E2CLRosiYZjpRiLEkhVpHj2II+D+PjA0vpqs060wTu6s8JvT8kZQyVftn6+ESwqKor29PS8jMaG/7aYkv972+jTiSqK++vXwir5UKtE5/kaNHjwYJo3b55Y8kdagvQGy2IgyrA0+LgFCxZQdna2OEdZuKfEV1g6G76ar/GAI2/9fi/ds2fPoeyEOa3RiRfWHT89yULepaDXcxXf/tsiBAHxKhKfrHHSLogy6uTvSufWlRjybdu2rZ+ha45OnTotexKVeVj2yaU2JeUmn481NOIhhIeHl3G5/j7vNQbWz3ge+S9hIey4Xb39FBveUMgZxNHS0tKG8ZW+BM8hwzh5Pc1FxUY3FPr5+Z1IS0sL4nLDQ1lZWXhYWNg6R/kpF3cPCuyVYzcQ1ZYcsU+kpqY2+H9wurKIX8BJc/m5GDXmU+q8/DL5xPVQ/L22hH+OiYmpLCwsbJiWZw72iXqOzlO9vLwe8a5ip2pDF3dPCn6j4LkskB9kNbuW+VxuGAGjLmjdunVsYGDgPkcN6boQ9/T397/A+eXbvP9CQ8dpzp85j7vmDCFxDx8fH8y7F/D+i/VHa1vgKVt0UlLSx9w5VYTENX19fasiIiL++f7770dz3cuJgoIC3169ehXxHHQzT6Wqn0dMnItFUxbtdHR0dFFJSUkE1786yMrKCs3MzBzHvnIlD/HdeBWAFR4Io0REU3x6wb7tZEJCwqbk5OSi9PT0V0s0W4B1ctDxnTJlyoicnJxivGJMSUkpzs7OLubIXty9e/eAQYMG+eJLsZpTNGjQoEGDBg0aNGjQoEGDhrqhUaP/A6V4Z6RVpcBwAAAAAElFTkSuQmCC"),
        ExportMetadata("BackgroundColor", "#FFFFC0"),
        ExportMetadata("PrimaryFontColor", "#0000C0"),
        ExportMetadata("SecondaryFontColor", "#0000FF")]
    public class UniversalSearchTool : PluginBase
    {
        public UniversalSearchTool()
        {
            // hook into the event that will fire when an Assembly fails to resolve
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(AssemblyResolveEventHandler);
        }
        public override IXrmToolBoxPluginControl GetControl()
        {
            return new UniversalSearch();
        }

        private Assembly AssemblyResolveEventHandler(object sender, ResolveEventArgs args)
        {
            Assembly loadAssembly = null;
            Assembly currAssembly = Assembly.GetExecutingAssembly();

            // base name of the assembly that failed to resolve
            var argName = args.Name.Substring(0, args.Name.IndexOf(","));

            // check to see if the failing assembly is one that we reference.
            List<AssemblyName> refAssemblies = currAssembly.GetReferencedAssemblies().ToList();
            var refAssembly = refAssemblies.Where(a => a.Name == argName).FirstOrDefault();

            // if the current unresolved assembly is referenced by our plugin, attempt to load
            if (refAssembly != null)
            {
                // load from the path to this plugin assembly, not host executable
                string dir = Path.GetDirectoryName(currAssembly.Location).ToLower();
                string folder = Path.GetFileNameWithoutExtension(currAssembly.Location);
                dir = Path.Combine(dir, folder);

                var assmbPath = Path.Combine(dir, $"{argName}.dll");

                if (File.Exists(assmbPath))
                {
                    loadAssembly = Assembly.LoadFrom(assmbPath);
                }
                else
                {
                    throw new FileNotFoundException($"Unable to locate dependency: {assmbPath}");
                }
            }

            return loadAssembly;
        }

    }
}