// ------------------------------------------------------------------------------
// 
// Copyright (c) 2005-2008 Swampware, Inc.
// 
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// 
// ------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Bamboo.VisualStudio.VisualStudio2008.CSharpProject
{
	/// <summary>
	/// Summary description for ProjectBuilder.
	/// </summary>
	public class ProjectBuilder
	{
		private OutputCollection _output = new OutputCollection();
		private ErrorCollection _errors = new ErrorCollection();
		private Bamboo.CSharp.Compilers.Compiler _compiler;
		private bool _succeeded;
		private string _assembly = null;

		public ProjectBuilder(Bamboo.CSharp.Compilers.Compiler compiler)
		{
			this._compiler = compiler;
		}

		public OutputCollection Output
		{
			get { return this._output; }
		}

		public ErrorCollection Errors
		{
			get { return this._errors; }
		}

		public bool Succeeded
		{
			get { return this._succeeded; }
		}

		public string Assembly
		{
			get { return this._assembly; }
		}

		public void Build(string filename, string configuration, ProjectBuildTracker projectBuildTracker)
		{
			System.IO.FileInfo fileInfo = new System.IO.FileInfo(filename);
			string name = fileInfo.Name.Substring( 0, fileInfo.Name.ToLower().LastIndexOf(".csproj") );
			System.IO.TextReader reader = fileInfo.OpenText();
			Models.Project project = ProjectReader.Read(name, reader);
			reader.Close();

			string projectFolder = LastLeft(filename, System.IO.Path.DirectorySeparatorChar.ToString());

			this.Build(project, configuration, projectFolder, projectBuildTracker);
		}

		public void Build(string filename, string configuration, string output, ProjectBuildTracker projectBuildTracker)
		{
			System.IO.FileInfo fileInfo = new System.IO.FileInfo(filename);
			string name = fileInfo.Name.Substring( 0, fileInfo.Name.ToLower().LastIndexOf(".csproj") );
			System.IO.TextReader reader = fileInfo.OpenText();
			Models.Project project = ProjectReader.Read(name, reader);
			reader.Close();

			string projectFolder = LastLeft(filename, System.IO.Path.DirectorySeparatorChar.ToString());

			if(!output.EndsWith(System.IO.Path.DirectorySeparatorChar.ToString()))
			{
				output += System.IO.Path.DirectorySeparatorChar;
			}

			string outputFolder = output;

			string extension = (project.Settings.OutputType.ToUpper().Equals("EXE") || project.Settings.OutputType.ToUpper().Equals("WINEXE")) ? ".exe" : ".dll";
			output += project.Settings.AssemblyName + extension;

			this.Build(project, configuration, projectFolder, output, outputFolder, projectBuildTracker);
		}

		private void Build(Bamboo.VisualStudio.VisualStudio2008.CSharpProject.Models.Project project, string configuration, string projectFolder, ProjectBuildTracker projectBuildTracker)
		{
			foreach (Bamboo.VisualStudio.VisualStudio2008.CSharpProject.Models.Config config in project.Configs)
			{
				string configName = "";

				if(config.Condition.IndexOf("Debug") != -1)
				{
					configName = "Debug";
				}
				else if(config.Condition.IndexOf("Release") != -1)
				{
					configName = "Release";
				}
				else
				{
					throw new System.Exception("Unknown configuration: " + config.Condition);
				}

				if(configName.ToLower().Equals(configuration.ToLower()))
				{
					string output = new System.IO.FileInfo(projectFolder).DirectoryName;
					if(!output.EndsWith(System.IO.Path.DirectorySeparatorChar.ToString()))
					{
						output += System.IO.Path.DirectorySeparatorChar;
					}

					string outputFolder = output;

					string extension = (project.Settings.OutputType.ToUpper().Equals("EXE") || project.Settings.OutputType.ToUpper().Equals("WINEXE")) ? ".exe" : ".dll";
					output += config.OutputPath + project.Settings.AssemblyName + extension;

					this.Build(project, configuration, projectFolder, output, outputFolder, projectBuildTracker);

					break;
				}
			}
		}

		private void Build(Bamboo.VisualStudio.VisualStudio2008.CSharpProject.Models.Project project, string configuration, string projectFolder, string output, string outputFolder, ProjectBuildTracker projectBuildTracker)
		{
			foreach (Bamboo.VisualStudio.VisualStudio2008.CSharpProject.Models.Config config in project.Configs)
			{
				string configName = "";

				if(config.Condition.IndexOf("Debug") != -1)
				{
					configName = "Debug";
				}
				else if(config.Condition.IndexOf("Release") != -1)
				{
					configName = "Release";
				}
				else
				{
					throw new System.Exception("Unknown configuration: " + config.Condition);
				}

				if(configName.ToLower().Equals(configuration.ToLower()))
				{
					BuildConfig(
						project.Name, 
						projectFolder, 
						project.ProjectGuid, 
						output, 
						outputFolder, 
						project.Settings, 
						config, 
						project.References, 
						project.ProjectReferences, 
						project.Items, 
						projectBuildTracker);

					return;
				}
			}

			throw new System.Exception("No project built.");
		}

		private void BuildConfig(
			string projectName, 
			string projectFolder, 
			System.Guid projectGuid, 
			string output, 
			string outputFolder,
			Bamboo.VisualStudio.VisualStudio2008.CSharpProject.Models.Settings settings,
			Bamboo.VisualStudio.VisualStudio2008.CSharpProject.Models.Config config,
			List<Bamboo.VisualStudio.VisualStudio2008.CSharpProject.Models.Reference> references,
			List<Bamboo.VisualStudio.VisualStudio2008.CSharpProject.Models.ProjectReference> projectReferences,
			List<Bamboo.VisualStudio.VisualStudio2008.CSharpProject.Models.Item> items, 
			ProjectBuildTracker projectBuildTracker)
		{
			this._output.Add(new Output("Begin Build " + projectName));
			this._output.Add(new Output(""));



			System.IO.DirectoryInfo directoryInfo = new System.IO.FileInfo(output).Directory;
			if(!directoryInfo.Exists)
			{
				directoryInfo.Create();
			}



			Bamboo.CSharp.Compilers.CompilerParameters compilerParameters = new Bamboo.CSharp.Compilers.CompilerParameters();

			if (settings.ApplicationIcon.Length > 0)
			{
				compilerParameters.ApplicationIcon = projectFolder + System.IO.Path.DirectorySeparatorChar + settings.ApplicationIcon;
			}

			compilerParameters.Assembly = output;

			compilerParameters.Target = settings.OutputType.ToLower();

			compilerParameters.Define = config.DefineConstants;
			if(config.DebugSymbols.Length == 0)
			{
				compilerParameters.Debug = false;
			}
			else
			{
				compilerParameters.Debug = System.Boolean.Parse(config.DebugSymbols);
			}
			foreach (Bamboo.VisualStudio.VisualStudio2008.CSharpProject.Models.Reference reference in references)
			{
				if( ProjectReferenceRules.IsFrameworkReference(reference) )
				{
					//
					// Framework reference.
					//
					string frameworkPath = this._compiler.GetFrameworkPath();
					string referencePath = frameworkPath + reference.Include + ".dll";

					if(!System.IO.File.Exists(referencePath))
					{
					}
					else
					{
						compilerParameters.References.Add(referencePath);
					}
				}
				else if( ProjectReferenceRules.IsAssemblyFolderReference(reference) )
				{
					//
					// AssemblyFolder reference.
					//
					string referencePath = ProjectReferenceRules.GetReferencePath(projectFolder, reference.HintPath, this._compiler);
					if(referencePath != null)
					{
						if(System.IO.File.Exists(referencePath))
						{
							compilerParameters.References.Add(referencePath);
						}
						else if(System.IO.File.Exists(referencePath + ".dll"))
						{
							compilerParameters.References.Add(referencePath + ".dll");
						}
						else
						{
						}
					}
				}
				else
				{
					//
					// File reference.
					//
					string referencePath = ProjectReferenceRules.GetReferencePath(projectFolder, reference.HintPath, this._compiler);
					if(referencePath != null)
					{
						string output_folder = new System.IO.FileInfo(output).DirectoryName;
						if(!output_folder.EndsWith(System.IO.Path.DirectorySeparatorChar.ToString()))
						{
							output_folder += System.IO.Path.DirectorySeparatorChar;
						}
						string target = output_folder + new System.IO.FileInfo(referencePath).Name;
						if(!target.Equals(referencePath))
						{
							System.IO.File.Copy(referencePath, target, true);

							while(!System.IO.File.Exists(target))
							{
								System.Threading.Thread.Sleep(500);
							}
						}

						compilerParameters.References.Add(referencePath);
					}
				}
			}
			foreach (Bamboo.VisualStudio.VisualStudio2008.CSharpProject.Models.ProjectReference projectReference in projectReferences)
			{
				//
				// Project reference.
				//
				string projectReferencePath = projectBuildTracker.GetPath(new System.Guid(projectReference.Project));

				if(projectReferencePath != null)
				{
					string output_folder = new System.IO.FileInfo(output).DirectoryName;
					if(!output_folder.EndsWith(System.IO.Path.DirectorySeparatorChar.ToString()))
					{
						output_folder += System.IO.Path.DirectorySeparatorChar;
					}
					string target = output_folder + new System.IO.FileInfo(projectReferencePath).Name;
					if(!target.Equals(projectReferencePath))
					{
						System.IO.File.Copy(projectReferencePath, target, true);

						while(!System.IO.File.Exists(target))
						{
							System.Threading.Thread.Sleep(500);
						}
					}
					compilerParameters.References.Add(target);
				}
			}
			foreach (Bamboo.VisualStudio.VisualStudio2008.CSharpProject.Models.Item item in items)
			{
				if(item.Type == "Compile")
				{
					compilerParameters.Sources.Add(projectFolder + System.IO.Path.DirectorySeparatorChar + item.Include);
				}
				else if(item.Type == "Content")
				{
					System.IO.FileInfo fileInfo = new System.IO.FileInfo(outputFolder + item.Include);
					if(!fileInfo.Directory.Exists)
					{
						fileInfo.Directory.Create();
					}
					System.IO.File.Copy(projectFolder + System.IO.Path.DirectorySeparatorChar + item.Include, outputFolder + item.Include, true);
				}
				else if(item.Type == "None" && item.Include.Equals("App.config"))
				{
					System.IO.File.Copy(projectFolder + System.IO.Path.DirectorySeparatorChar + item.Include, output + ".config");
				}
				else if(item.Type == "EmbeddedResource")
				{
					if(item.Include.ToLower().EndsWith(".resx"))
					{
						string RESGEN_EXE = Bamboo.CSharp.FrameworkDetector.GetSDKInstallRoot("v" + this._compiler.GetFrameworkVersion()) + "Bin\\resgen.exe";
						//TODO OLD string RESGEN_EXE = @"C:\Program Files\Swampware\SDK\NET_2.0\resgen.exe";

						System.Diagnostics.ProcessStartInfo processStartInfo = new System.Diagnostics.ProcessStartInfo();
						processStartInfo.FileName = RESGEN_EXE;
						processStartInfo.RedirectStandardOutput = true;
						processStartInfo.UseShellExecute = false;
						processStartInfo.CreateNoWindow = true;
						processStartInfo.WorkingDirectory = System.Environment.CurrentDirectory;

						string arguments = "\"" + projectFolder + System.IO.Path.DirectorySeparatorChar + item.Include + "\"";

						string resourceFile = item.Include.Substring(0, item.Include.Length - 5) + ".resources";
						string resource_name = settings.RootNamespace + "." + resourceFile.Replace(System.IO.Path.DirectorySeparatorChar.ToString(), ".");
						string resource_path = "\"" + projectFolder + System.IO.Path.DirectorySeparatorChar + "obj" + System.IO.Path.DirectorySeparatorChar + "res" + System.IO.Path.DirectorySeparatorChar + resource_name + "\"";
						arguments += " " + resource_path;

						if(!System.IO.Directory.Exists(projectFolder + System.IO.Path.DirectorySeparatorChar + "obj" + System.IO.Path.DirectorySeparatorChar + "res"))
						{
							System.IO.Directory.CreateDirectory(projectFolder + System.IO.Path.DirectorySeparatorChar + "obj" + System.IO.Path.DirectorySeparatorChar + "res");
						}

						processStartInfo.Arguments = arguments;
						System.Diagnostics.Process process = System.Diagnostics.Process.Start(processStartInfo);

						System.IO.StringWriter stringWriter = new System.IO.StringWriter();
						while(!process.HasExited)
						{
							stringWriter.Write(process.StandardOutput.ReadToEnd());
							process.WaitForExit(50);
						}
						stringWriter.Write(process.StandardOutput.ReadToEnd());
						process.Close();
						process.Dispose();

						this._output.Add(new Output(stringWriter.ToString()));




						this._output.Add(new Output("Converting " + item.Include + " to " + resourceFile + ""));

						compilerParameters.Resources.Add(resource_path);
					}
					else
					{
						string resource_name = settings.RootNamespace + "." + item.Include.Replace(System.IO.Path.DirectorySeparatorChar.ToString(), ".");

						compilerParameters.Resources.Add("\"" + projectFolder + System.IO.Path.DirectorySeparatorChar + item.Include + "\"" + "," + resource_name);
					}
				}
			}


			this._compiler.Compile(compilerParameters);



			//
			// Output
			//
			foreach(string line in compilerParameters.Output)
			{
				this._output.Add(new Output(line));
			}

			//
			// Errors
			//
			bool hasErrors = false;
			foreach(Bamboo.CSharp.Compilers.Error error in compilerParameters.Errors)
			{
				if(!error.IsWarning)
				{
					hasErrors = true;
				}
				this._errors.Add(new Error(error.IsWarning, error.File, error.Line, error.Column, error.Code, error.Description, error.Text));
			}



			if(hasErrors)
			{
				this._succeeded = false;
				this._output.Add(new Output(""));
				this._output.Add(new Output("Build Failed " + projectName));
			}
			else
			{
				this._succeeded = true;
				this._assembly = compilerParameters.Assembly;
				this._output.Add(new Output(""));
				this._output.Add(new Output("Build Succeeded " + projectName));

				projectBuildTracker.Add(projectGuid, compilerParameters.Assembly);
			}
		}

		private static string LastLeft(string input, string marker)
		{
			int index = input.LastIndexOf(marker);
			if(index == -1)
			{
				return input;
			}
			return input.Substring(0, index);
		}

	}
}
