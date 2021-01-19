//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ECMSS.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class FileInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FileInfo()
        {
            this.FileHistories = new HashSet<FileHistory>();
            this.FileFavorites = new HashSet<FileFavorite>();
            this.FileImportants = new HashSet<FileImportant>();
            this.Trashes = new HashSet<Trash>();
            this.FileShares = new HashSet<FileShare>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int Owner { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string Tag { get; set; }
        public int DirectoryId { get; set; }
    
        public virtual Directory Directory { get; set; }
        public virtual Employee Employee { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FileHistory> FileHistories { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FileFavorite> FileFavorites { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FileImportant> FileImportants { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Trash> Trashes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FileShare> FileShares { get; set; }
    }
}
