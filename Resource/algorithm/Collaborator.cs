//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UP_Research.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Collaborator
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Collaborator()
        {
            this.ProjectCollaboration = new HashSet<ProjectCollaboration>();
        }
    
        public int CollaboratorID { get; set; }
        public string Initials { get; set; }
        public string Surname { get; set; }
        public string CollaboratorType { get; set; }
        public int TitleID { get; set; }
        public int ID_ID { get; set; }
        public string Name { get; set; }
        public Nullable<bool> IsActive { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProjectCollaboration> ProjectCollaboration { get; set; }
        public virtual InstitutionDepartment InstitutionDepartment { get; set; }
        public virtual Title Title { get; set; }
    }
}