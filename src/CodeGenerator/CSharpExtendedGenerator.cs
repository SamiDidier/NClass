using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
//using NClass.CodeGenerator.Templates;
using ICSharpCode.NRefactory.CSharp;
using NStub.CSharp;
using System.Windows.Forms;
using NClass.Core;


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

    class CSharpExtendedGenerator
    {
        public void Generate()
        {
            
        }

        private void GenerateCSharpFile(string projectName, string outputDirectory, bool xmlDocFood, bool generetaNUnit, FormatStyleEnum formatIndex, string formatFile, bool sortUsing, int templateIndex)
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
            if (generetaNUnit == true)
            {
                // Generate the project
                NStub.CSharp.CSharpProjectGenerator gen = new NStub.CSharp.CSharpProjectGenerator(String.Format("{0}_unitary_tests", projectName), outputDirectory);
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
            CSharpParser parser = new CSharpParser();

            // Open the C# source file to read
            using (TextReader sr = new StreamReader(fileName))
            {
                // TO DO: TextEditorOptions
                CSharpFormatter formater = new CSharpFormatter (formatStyle);
                formater.Format(sr.ReadToEnd());

                // Write the new C# source file if modified

            }
        }
    }
}
