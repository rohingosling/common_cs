﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.296
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GenomeNotepad {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class StringTable {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal StringTable() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("GenomeNotepad.StringTable", typeof(StringTable).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to  : .
        /// </summary>
        internal static string AddressStringSeparator {
            get {
                return ResourceManager.GetString("AddressStringSeparator", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to    .
        /// </summary>
        internal static string AddressStringTerminator {
            get {
                return ResourceManager.GetString("AddressStringTerminator", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SEQX.
        /// </summary>
        internal static string FileExtentionExtendedPlainSequence {
            get {
                return ResourceManager.GetString("FileExtentionExtendedPlainSequence", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to GSS.
        /// </summary>
        internal static string FileExtentionGenomeStudio {
            get {
                return ResourceManager.GetString("FileExtentionGenomeStudio", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SEQ.
        /// </summary>
        internal static string FileExtentionPlainSequence {
            get {
                return ResourceManager.GetString("FileExtentionPlainSequence", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to All files (*.*)|*.*.
        /// </summary>
        internal static string FileFilterAllFiles {
            get {
                return ResourceManager.GetString("FileFilterAllFiles", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Extended plain sequence files (*.SEQX)|*.SEQX.
        /// </summary>
        internal static string FileFilterExtendedPlainSequence {
            get {
                return ResourceManager.GetString("FileFilterExtendedPlainSequence", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Genome Studio sequence files (*.GSS)|*.GSS.
        /// </summary>
        internal static string FileFilterGenomeStudio {
            get {
                return ResourceManager.GetString("FileFilterGenomeStudio", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Plain sequence files (*.SEQ)|*.SEQ.
        /// </summary>
        internal static string FileFilterPlainSequence {
            get {
                return ResourceManager.GetString("FileFilterPlainSequence", resourceCulture);
            }
        }
    }
}