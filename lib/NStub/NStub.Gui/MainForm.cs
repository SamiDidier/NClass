using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using NStub.Core;
using NStub.CSharp;

namespace NStub.Gui
{
	/// <summary>e
	/// This is the main UI form for the NStub application.
	/// </summary>
	public partial class MainForm : Form
	{
		#region Member Variables (Private)

		private IList<AssemblyName> _referencedAssemblies =
			new List<AssemblyName>();

		#endregion Member Variables (Private)

		#region Constructor (Public)

		/// <summary>
		/// Initializes a new instance of the <see cref="MainForm"/> class.
		/// </summary>
		public MainForm()
		{
			InitializeComponent();
		}

		#endregion Constructor (Public)

		#region Event Handlers (Private)

		/// <summary>
		/// Handles the Click event of the btnBrowseInputAssembly control.
		/// Allows the user to select the assembly they wish to generate the test cases
		/// for.  Calls the <see cref="LoadAssembly"/> method 
		/// to query the assembly for its contained types.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the 
		/// event data.</param>
		private void btnBrowseInputAssembly_Click(object sender, EventArgs e)
		{
			if (_inputAssemblyOpenFileDialog.ShowDialog() == DialogResult.OK)
			{
				_inputAssemblyTextBox.Text = _inputAssemblyOpenFileDialog.FileName;
				_browseOutputDirectoryButton.Enabled = true;

				Cursor swapCursor = Cursor.Current;
				Cursor.Current = Cursors.WaitCursor;

				LoadAssembly();

				Cursor.Current = swapCursor;
			}
		}

		/// <summary>
		/// Handles the Click event of the btnBrowseOutputDirectory control.
		///	Allows the user to select the output directory for the generated sources
		/// files.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the 
		/// event data.</param>
		private void btnBrowseOutputDirectory_Click(object sender, EventArgs e)
		{
			if (_outputFolderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				_outputDirectoryTextBox.Text = _outputFolderBrowserDialog.SelectedPath;
				_goButton.Enabled = true;
			}
		}

		/// <summary>
		/// Handles the BeforeSelect event of the tvAssemblyGraph control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="T:System.Windows.Forms.TreeViewCancelEventArgs"/> 
		/// instance containing the event data.</param>
		private void tvAssemblyGraph_BeforeSelect(object sender, TreeViewCancelEventArgs e)
		{
			//tvAssemblyGraph.SelectedImageIndex = e.Node.ImageIndex;
		}

		/// <summary>
		/// Handles the Click event of the btnGo control.  Creates a list of the methods
		/// for which the user wishes to generate test cases for and instantiates an
		/// instance of NStub around these methods.  The sources are files are then
		/// generated.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the 
		/// event data.</param>
		private void btnGo_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			_browseInputAssemblyButton.Enabled = false;
			_browseOutputDirectoryButton.Enabled = false;

			// Create a new directory for each assembly
			for (int h = 0; h < _assemblyGraphTreeView.Nodes.Count; h++)
			{
				string outputDirectory = _outputDirectoryTextBox.Text +
				                         Path.DirectorySeparatorChar +
				                         Path.GetFileNameWithoutExtension(_assemblyGraphTreeView.Nodes[h].Text) + "Test";
				Directory.CreateDirectory(outputDirectory);

				// Create our project generator
				CSharpProjectGenerator cSharpProjectGenerator =
					new CSharpProjectGenerator(
						Path.GetFileNameWithoutExtension(_inputAssemblyTextBox.Text) + "Test",
						outputDirectory);

				// Add our referenced assemblies to the project generator so we
				// can build the resulting project
				foreach (AssemblyName assemblyName in _referencedAssemblies)
				{
					cSharpProjectGenerator.ReferencedAssemblies.Add(assemblyName);
				}

				// Generate the new test namespace
				// At the assembly level
				for (int i = 0; i < _assemblyGraphTreeView.Nodes[h].Nodes.Count; i++)
				{
					if (_assemblyGraphTreeView.Nodes[h].Nodes[i].Checked)
					{
						// Create the namespace and initial inputs
						CodeNamespace codeNamespace = new CodeNamespace();

						// At the type level
						for (int j = 0; j < _assemblyGraphTreeView.Nodes[h].Nodes[i].Nodes.Count; j++)
						{
							//TODO: This namespace isn't being set correctly.  
							// Also one namespace per run probably won't work, we may 
							// need to break this up more.
							codeNamespace.Name =
								Utility.GetNamespaceFromFullyQualifiedTypeName(
									_assemblyGraphTreeView.Nodes[h].Nodes[i].Nodes[j].Text);

							if (_assemblyGraphTreeView.Nodes[h].Nodes[i].Nodes[j].Checked)
							{
								// Create the class
								CodeTypeDeclaration testClass =
									new CodeTypeDeclaration(_assemblyGraphTreeView.Nodes[h].Nodes[i].Nodes[j].Text);
								codeNamespace.Types.Add(testClass);

								// At the method level
								// Create a test method for each method in this type
								for (int k = 0; k < _assemblyGraphTreeView.Nodes[h].Nodes[i].Nodes[j].Nodes.Count; k++)
								{
									try
									{
										if (_assemblyGraphTreeView.Nodes[h].Nodes[i].Nodes[j].Nodes[k].Checked)
										{
											// Retrieve the MethodInfo object from this TreeNode's tag
											CodeMemberMethod codeMemberMethod =
												CreateMethod(
													_assemblyGraphTreeView.Nodes[h].Nodes[i].Nodes[j].Nodes[k].Text,
													(MethodInfo) _assemblyGraphTreeView.Nodes[h].Nodes[i].Nodes[j].Nodes[k].Tag);
											testClass.Members.Add(codeMemberMethod);
										}
									}
									catch (Exception)
									{
										return;
									}
								}
							}
						}
						// Now write the test file
						NStubCore nStub =
							new NStubCore(codeNamespace, outputDirectory,
							              new CSharpCodeGenerator(codeNamespace, outputDirectory));
						nStub.GenerateCode();

						// Add all of our classes to the project
						foreach (CodeTypeDeclaration codeType in nStub.CodeNamespace.Types)
						{
							string fileName = codeType.Name;
							fileName = fileName.Remove(0, (fileName.LastIndexOf(".") + 1));
							fileName += ".cs";
							cSharpProjectGenerator.ClassFiles.Add(fileName);
						}
					}
				}
				// Now write the project file and add all of the test files to it
				cSharpProjectGenerator.GenerateProjectFile();
			}

			_browseInputAssemblyButton.Enabled = true;
			_browseOutputDirectoryButton.Enabled = true;
			Cursor.Current = Cursors.Arrow;
		}

		/// <summary>
		/// Handles the AfterCheck event of the tvAssemblyGraph control.
		/// Checks or unchecks all children of 
		/// <see cref="System.Windows.Forms.TreeViewEventArgs.Node">e.Node</see>.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="T:System.Windows.Forms.TreeViewEventArgs"/> 
		/// instance containing the event data.</param>
		private void tvAssemblyGraph_AfterCheck(object sender, TreeViewEventArgs e)
		{
			// Apply this choice to the Node's children
			CheckChildren(e.Node);
		}

		#endregion Event Handlers (Private)

		#region Helper Methods (Private)

		/// <summary>
		/// Creates a <see cref="System.Windows.Forms.TreeNode">TreeNode</see> with the 
		/// given text and image key.
		/// </summary>
		/// <param name="text">The text of the TreeNode.</param>
		/// <param name="imageKey">The key corresponding to the TreeNode's image.</param>
		/// <returns></returns>
		private TreeNode CreateTreeNode(string text, string imageKey)
		{
			TreeNode treeNode = new TreeNode(text);
			treeNode.Checked = true;
			treeNode.ImageIndex = _objectIconsImageList.Images.IndexOfKey(imageKey);

			return treeNode;
		}

		/// <summary>
		/// Reflects through the currently selected assembly and reflects the type tree
		/// in tvAssemblyGraph.
		/// </summary>
		private void LoadAssembly()
		{
			_assemblyGraphTreeView.Nodes.Clear();

			for (int theAssembly = 0; theAssembly < _inputAssemblyOpenFileDialog.FileNames.Length; theAssembly++)
			{
				// Load our input assembly and create its node in the tree
				Assembly inputAssembly =
					Assembly.LoadFile(_inputAssemblyOpenFileDialog.FileNames[theAssembly]);
				TreeNode assemblyTreeNode =
					CreateTreeNode(_inputAssemblyOpenFileDialog.FileNames[theAssembly],
					               "imgAssembly");
				_assemblyGraphTreeView.Nodes.Add(assemblyTreeNode);

				// Add our referenced assemblies to the project generator so we
				// can reference them later
				foreach (AssemblyName assemblyName in inputAssembly.GetReferencedAssemblies())
				{
					_referencedAssemblies.Add(assemblyName);
				}

				// Retrieve the modules from the assembly.  Most assemblies only have one
				// module, but it is possible for assemblies to possess multiple modules
				Module[] modules = inputAssembly.GetModules(false);

				// Add the namespaces in the DLL
				for (int theModule = 0; theModule < modules.Length; theModule++)
				{
					// Add a node to the tree to represent the module
					TreeNode moduleTreeNode =
						CreateTreeNode(modules[theModule].Name, "imgModule");
					_assemblyGraphTreeView.Nodes[theAssembly].Nodes.Add(moduleTreeNode);
					Type[] containedTypes = modules[theModule].GetTypes();

					// Add the classes in each type
					for (int theClass = 0; theClass < containedTypes.Length; theClass++)
					{
						// Add a node to the tree to represent the class
						_assemblyGraphTreeView.Nodes[theAssembly].Nodes[theModule].Nodes.Add(
							CreateTreeNode(containedTypes[theClass].FullName, "imgClass"));

						// Create a test method for each method in this type
						MethodInfo[] methods = containedTypes[theClass].GetMethods();
						for (int theMethod = 0; theMethod < methods.Length; theMethod++)
						{
							_assemblyGraphTreeView.Nodes[theAssembly].Nodes[theModule].Nodes[theClass].Nodes.Add(
								CreateTreeNode(methods[theMethod].Name, "imgMethod"));

							// Store the method's MethodInfo object in this node's tag
							// so that we may retrieve it later
							_assemblyGraphTreeView.Nodes[theAssembly].Nodes[theModule].Nodes[theClass].Nodes[theMethod].Tag =
								methods[theMethod];
						}
					}
					moduleTreeNode.Expand();
				}
				assemblyTreeNode.Expand();
			}
		}

		/// <summary>
		/// Applies the check state of the given 
		/// <see cref="System.Windows.Forms.TreeNode">TreeNode</see> to all of its 
		/// children.
		/// </summary>
		/// <param name="treeNode">The TreeNode which contains the check state to apply.</param>
		private void CheckChildren(TreeNode treeNode)
		{
			if (treeNode.Nodes.Count != 0)
			{
				CheckChildren(treeNode.Nodes[0]);
				for (int i = 0; i < treeNode.Nodes.Count; i++)
				{
					treeNode.Nodes[i].Checked = treeNode.Checked;
				}
			}
		}

		private CodeNamespace CreateNamespace(TreeNode treeNode)
		{
			return null;
		}

		private CodeMemberMethod CreateMethod(string methodName, MethodInfo methodInfo)
		{
			// Create the method
			CodeMemberMethod codeMemberMethod = new CodeMemberMethod();

			codeMemberMethod.Attributes = (MemberAttributes) methodInfo.Attributes;
			codeMemberMethod.Name = methodName;

			// Set the return type for the method
			codeMemberMethod.ReturnType = new CodeTypeReference(methodInfo.ReturnType);

			// Setup and add the parameters
			ParameterInfo[] methodParameters = methodInfo.GetParameters();
			foreach (ParameterInfo parameter in methodParameters)
			{
				codeMemberMethod.Parameters.Add(
					new CodeParameterDeclarationExpression(parameter.ParameterType,
					                                       parameter.Name));
			}

			return codeMemberMethod;
		}

		#endregion Helper Methods (Private)
	}
}