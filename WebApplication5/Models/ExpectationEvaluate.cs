//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication5.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ExpectationEvaluate
    {
        public string ExpectationEvaluateId { get; set; }
        public string Log { get; set; }
        public string Point { get; set; }
        public string ExpectationId { get; set; }
        public string AssignmentId { get; set; }
        public string StudentId { get; set; }
    
        public virtual Assignment Assignment { get; set; }
        public virtual Assignment Assignment1 { get; set; }
        public virtual Expectation Expectation { get; set; }
    }
}
