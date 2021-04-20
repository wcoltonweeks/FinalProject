using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FInalProject.DATA.EF//.JobBoardMetadata
{
    #region Application
    
    public class ApplicationMetadata
    {
        public int ApplicationID { get; set; }
        [Display(Name ="Position")]
        [Required(ErrorMessage ="Position is required")]
        public int OpenPositionID { get; set; }
        [Required(ErrorMessage ="User ID is required")]
        [Display(Name ="User ID")]
        public string UserID { get; set; }
        [Required(ErrorMessage ="Date is required")]
        [Display(Name ="Application Date")]
        public System.DateTime ApplicationDate { get; set; }
        [UIHint("MultilineText")]
        [Display(Name ="Manager Notes")]
        [StringLength(2000, ErrorMessage ="Must not exceed 2000 characters")]
        public string ManagerNotes { get; set; }
        [Display(Name ="Application Status")]
        [Required(ErrorMessage ="Status is required")]
        public int ApplicationStatus { get; set; }
        [Display(Name ="Resume")]
        [Required(ErrorMessage ="Resume is required")]
        public string ResumeFilename { get; set; }
        [UIHint("MultilineText")]
        [Display(Name ="Employee Notes")]
        [StringLength(500, ErrorMessage ="Must not exceed 500 characters")]
        public string EmployeeNotes { get; set; }
    }

    [MetadataType(typeof(ApplicationMetadata))]
    public partial class Application
    {

    }
    #endregion

    #region ApplicationStatus

    public class ApplicationStatusMetadata
    {
        public int ApplicationStatusID { get; set; }
        [Display(Name ="Status Name")]
        [Required(ErrorMessage ="Status Name is required")]
        [StringLength(50, ErrorMessage ="Must not exceed 50 characters")]
        public string StatusName { get; set; }
        [Display(Name ="Status Description")]
        [StringLength(250, ErrorMessage ="Must not exceed 250 chsracters")]
        public string StatusDescription { get; set; }
    }

    [MetadataType(typeof(ApplicationStatusMetadata))]
    public partial class ApplicationStatus
    {

    }
    #endregion

    #region Location

    public class LocationMetadata
    {
        public int LocationID { get; set; }
        [Required(ErrorMessage ="Store Number is reqiored")]
        [Display(Name ="Store Number")]
        [StringLength(10, ErrorMessage ="Must not exceed 10 characters")]
        public string StoreNumber { get; set; }        
        [Required(ErrorMessage ="City is required")]
        [StringLength(50, ErrorMessage = "Must not exceed 50 characters")]
        public string City { get; set; }
        [Required(ErrorMessage ="State is required")]
        [StringLength(2, ErrorMessage = "Must not exceed 2 characters")]
        public string State { get; set; }
        [Display(Name ="Manager ID")]
        [Required(ErrorMessage ="Manager ID is required")]
        public string ManagerID { get; set; }
    }

    [MetadataType(typeof(LocationMetadata))]
    public partial class Location
    {

    }
    #endregion

    #region Position
    
    public class PositionMetadata
    {
        public int PositionID { get; set; }
        [Required(ErrorMessage ="Position Title is required")]
        [StringLength(50, ErrorMessage = "Must not exceed 50 characters")]
        public string Title { get; set; }
        [StringLength(8000, ErrorMessage = "Must not exceed 8000 characters")]
        public string Description { get; set; }
    }

    [MetadataType(typeof(PositionMetadata))]
    public partial class Position
    {

    }
    #endregion

    #region UserDetail

    public class UserDetailMetadata
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "* First Name is required")]
        [StringLength(50, ErrorMessage = "* First Name mnust not exceed 50 characters")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "* Last Name is required")]
        [StringLength(50, ErrorMessage = "* Last Name mnust not exceed 50 characters")]
        public string LastName { get; set; }

        [Display(Name = "Resume")]
        public string ResumeFilename { get; set; }
    }

    [MetadataType(typeof(UserDetailMetadata))]
    public partial class UserDetail
    {

    }
    #endregion
}
