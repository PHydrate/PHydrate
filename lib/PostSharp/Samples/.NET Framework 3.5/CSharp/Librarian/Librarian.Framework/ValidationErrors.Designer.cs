﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4927
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Librarian.Framework {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ValidationErrors {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ValidationErrors() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Librarian.Framework.ValidationErrors", typeof(ValidationErrors).Assembly);
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
        ///   Looks up a localized string similar to The field {0}.{1} cannot be of type {2}. Use the type EntityRef&lt;{2}&gt; instead..
        /// </summary>
        internal static string LF0001 {
            get {
                return ResourceManager.GetString("LF0001", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The field {0}.{1} cannot be of type {2}. Entity field type should be either EntityRefs, either a value type, either should implement ICloneable..
        /// </summary>
        internal static string LF0002 {
            get {
                return ResourceManager.GetString("LF0002", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The field {0}.{1} cannot be of type {2}. Entity field type should be serializable..
        /// </summary>
        internal static string LF0003 {
            get {
                return ResourceManager.GetString("LF0003", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The entity type {0} should be serializable..
        /// </summary>
        internal static string LF0004 {
            get {
                return ResourceManager.GetString("LF0004", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot apply the custom attribute [FreezeForm] on the method {0} because it is static..
        /// </summary>
        internal static string LF0005 {
            get {
                return ResourceManager.GetString("LF0005", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot apply the custom attribute [FreezeForm] on the type {1} because  it is not derived from Control..
        /// </summary>
        internal static string LF0006 {
            get {
                return ResourceManager.GetString("LF0006", resourceCulture);
            }
        }
    }
}