﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gmou.DataRepository.EntityRepository
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class GMOUMISEntity : DbContext
    {
        public GMOUMISEntity()
            : base("name=GMOUMISEntity")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tblBu> tblBus { get; set; }
        public virtual DbSet<tblBusInsuranceDeatil> tblBusInsuranceDeatils { get; set; }
        public virtual DbSet<tblSet> tblSets { get; set; }
        public virtual DbSet<tblEmployeeQuick> tblEmployeeQuicks { get; set; }
        public virtual DbSet<tblMasterDepartment> tblMasterDepartments { get; set; }
        public virtual DbSet<tbl_CashVivrani> tbl_CashVivrani { get; set; }
        public virtual DbSet<tbl_masterDepo> tbl_masterDepo { get; set; }
        public virtual DbSet<tbl_ChitFuel> tbl_ChitFuel { get; set; }
        public virtual DbSet<tblBusDetail> tblBusDetails { get; set; }
        public virtual DbSet<tblBusOwnerDetail> tblBusOwnerDetails { get; set; }
        public virtual DbSet<tbl_finalStock> tbl_finalStock { get; set; }
        public virtual DbSet<tmp_cashvivrani> tmp_cashvivrani { get; set; }
        public virtual DbSet<tbl_ShortName> tbl_ShortName { get; set; }
        public virtual DbSet<tbl_bustransfer> tbl_bustransfer { get; set; }
    
        public virtual ObjectResult<sp_GetButDebitStatusByDate_Result> sp_GetButDebitStatusByDate()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetButDebitStatusByDate_Result>("sp_GetButDebitStatusByDate");
        }
    }
}