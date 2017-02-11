// ------------------------------------------------------------------------------
// 
// Copyright (c) 2006 Swampware, Inc.
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

namespace Bamboo.VisualStudio.VisualStudio2005.CSharpProject.Models
{
	/// <summary>
	/// Summary description for Settings.
	/// </summary>
	public class Settings
	{
		private System.String _applicationicon = String.Empty;
		private System.String _configuration = String.Empty;
		private System.String _configurationCondition = String.Empty;
		private System.String _platform = String.Empty;
		private System.String _platformCondition = String.Empty;
		private System.String _productVersion = String.Empty;
		private System.String _schemaVersion = String.Empty;
		private System.String _outputType = String.Empty;
		private System.String _appDesignerFolder = String.Empty;
		private System.String _rootNamespace = String.Empty;
		private System.String _assemblyName = String.Empty;
		private System.String _isWebBootstrapper = String.Empty;
		private System.String _signManifests = String.Empty;
		private System.String _generateManifests = String.Empty;
		private System.String _manifestCertificateThumbprint = String.Empty;
		private System.String _manifestKeyFile = String.Empty;
		private System.String _publishUrl = String.Empty;
		private System.String _install = String.Empty;
		private System.String _installFrom = String.Empty;
		private System.String _updateEnabled = String.Empty;
		private System.String _updateMode = String.Empty;
		private System.String _updateInterval = String.Empty;
		private System.String _updateIntervalUnits = String.Empty;
		private System.String _updatePeriodically = String.Empty;
		private System.String _updateRequired = String.Empty;
		private System.String _mapFileExtensions = String.Empty;
		private System.String _productName = String.Empty;
		private System.String _publisherName = String.Empty;
		private System.String _applicationVersion = String.Empty;
		private System.String _bootstrapperEnabled = String.Empty;

		public Settings()
		{
		}

		public System.String ApplicationIcon
		{
			get { return this._applicationicon; }
			set { this._applicationicon = value; }
		}

		public System.String Configuration
		{
			get { return this._configuration; }
			set { this._configuration = value; }
		}

		public System.String ConfigurationCondition
		{
			get { return this._configurationCondition; }
			set { this._configurationCondition = value; }
		}

		public System.String Platform
		{
			get { return this._platform; }
			set { this._platform = value; }
		}

		public System.String PlatformCondition
		{
			get { return this._platformCondition; }
			set { this._platformCondition = value; }
		}

		public System.String ProductVersion
		{
			get { return this._productVersion; }
			set { this._productVersion = value; }
		}

		public System.String SchemaVersion
		{
			get { return this._schemaVersion; }
			set { this._schemaVersion = value; }
		}

		public System.String OutputType
		{
			get { return this._outputType; }
			set { this._outputType = value; }
		}

		public System.String AppDesignerFolder
		{
			get { return this._appDesignerFolder; }
			set { this._appDesignerFolder = value; }
		}

		public System.String RootNamespace
		{
			get { return this._rootNamespace; }
			set { this._rootNamespace = value; }
		}

		public System.String AssemblyName
		{
			get { return this._assemblyName; }
			set { this._assemblyName = value; }
		}

		public System.String IsWebBootstrapper
		{
			get { return this._isWebBootstrapper; }
			set { this._isWebBootstrapper = value; }
		}

		public System.String SignManifests
		{
			get { return this._signManifests; }
			set { this._signManifests = value; }
		}

		public System.String GenerateManifests
		{
			get { return this._generateManifests; }
			set { this._generateManifests = value; }
		}

		public System.String ManifestCertificateThumbprint
		{
			get { return this._manifestCertificateThumbprint; }
			set { this._manifestCertificateThumbprint = value; }
		}

		public System.String ManifestKeyFile
		{
			get { return this._manifestKeyFile; }
			set { this._manifestKeyFile = value; }
		}

		public System.String PublishUrl
		{
			get { return this._publishUrl; }
			set { this._publishUrl = value; }
		}

		public System.String Install
		{
			get { return this._install; }
			set { this._install = value; }
		}

		public System.String InstallFrom
		{
			get { return this._installFrom; }
			set { this._installFrom = value; }
		}

		public System.String UpdateEnabled
		{
			get { return this._updateEnabled; }
			set { this._updateEnabled = value; }
		}

		public System.String UpdateMode
		{
			get { return this._updateMode; }
			set { this._updateMode = value; }
		}

		public System.String UpdateInterval
		{
			get { return this._updateInterval; }
			set { this._updateInterval = value; }
		}

		public System.String UpdateIntervalUnits
		{
			get { return this._updateIntervalUnits; }
			set { this._updateIntervalUnits = value; }
		}

		public System.String UpdatePeriodically
		{
			get { return this._updatePeriodically; }
			set { this._updatePeriodically = value; }
		}

		public System.String UpdateRequired
		{
			get { return this._updateRequired; }
			set { this._updateRequired = value; }
		}

		public System.String MapFileExtensions
		{
			get { return this._mapFileExtensions; }
			set { this._mapFileExtensions = value; }
		}

		public System.String ProductName
		{
			get { return this._productName; }
			set { this._productName = value; }
		}

		public System.String PublisherName
		{
			get { return this._publisherName; }
			set { this._publisherName = value; }
		}

		public System.String ApplicationVersion
		{
			get { return this._applicationVersion; }
			set { this._applicationVersion = value; }
		}

		public System.String BootstrapperEnabled
		{
			get { return this._bootstrapperEnabled; }
			set { this._bootstrapperEnabled = value; }
		}

	}
}
