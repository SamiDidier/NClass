using System.IO;
using ICSharpCode.NRefactory.CSharp;
//using NClass.CodeGenerator.Templates;


namespace NClass.CodeGenerator
{
    public enum FormatStyleEnum
    {
        Empty = 0,
        Mono,
        KR_style,
        Allman_Visual_Studio,
        Whitesmiths,
        GNU,
        Custom
    }

    internal class CSharpExtendedGenerator
    {
        public void Generate()
        {
        }

        private void GenerateCSharpFile(string projectName,
                                        string outputDirectory,
                                        bool xmlDocFood,
                                        bool generetaNUnit,
                                        FormatStyleEnum formatIndex,
                                        string formatFile,
                                        bool sortUsing,
                                        int templateIndex)
        {
            // To DO
            // For each file
            // http://msdn.microsoft.com/en-us/library/ee844259.aspx
            //TemplateStandard page = new TemplateStandard(xmlDocFood, sortUsing);
            //String pageContent = page.TransformText();
            //System.IO.File.WriteAllText("outputPage.cs", pageContent);

            // Format this new C# source file
            CSharpFormattingOptions format;
            switch (formatIndex)
            {
                case FormatStyleEnum.Empty:
                    // Nothing to do!
                    // format = FormattingOptionsFactory.CreateEmpty();
                    break;
                case FormatStyleEnum.Mono:
                    format = FormattingOptionsFactory.CreateMono();
                    break;
                case FormatStyleEnum.KR_style:
                    format = FormattingOptionsFactory.CreateKRStyle();
                    break;
                case FormatStyleEnum.Allman_Visual_Studio:
                    format = FormattingOptionsFactory.CreateAllman();
                    break;
                case FormatStyleEnum.Whitesmiths:
                    format = FormattingOptionsFactory.CreateWhitesmiths();
                    break;
                case FormatStyleEnum.GNU:
                    format = FormattingOptionsFactory.CreateGNU();
                    break;
                case FormatStyleEnum.Custom:
                    format = (CSharpFormattingOptions) CSharpFormattingOptionsUI.Load(formatFile);
                    break;
                default:
                    // unknow value!
                    // TO DO : Throw an error
                    break;
            }

            // Genereta NUnit test class
            if (generetaNUnit)
            {
                // Generate the project
                var gen = new NStub.CSharp.CSharpProjectGenerator(string.Format("{0}_unitary_tests", projectName),
                                                                  outputDirectory);
                // gen.ReferencedAssemblies

                gen.GenerateProjectFile();

                /*
                // Generate the test case file
                foreach(string origFile in )
                {
                    CSharpCodeGenerator unitFile = new CSharpCodeGenerator(origFile, origFile);
                    unitFile.GenerateCode();
                }
                */
            }
        }

        private void FormatSourceCode(string fileName, CSharpFormattingOptions formatStyle)
        {
            var parser = new CSharpParser();

            // Open the C# source file to read
            using (TextReader sr = new StreamReader(fileName))
            {
                // TO DO: TextEditorOptions
                var formater = new CSharpFormatter(formatStyle);
                formater.Format(sr.ReadToEnd());

                // Write the new C# source file if modified
            }
        }
    }
}