using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;

namespace DomainObjects
{
    public class BaseDomainObject<DomainObjectType> : ActiveRecordBase<DomainObjectType>, IAuditable
    {
        [Property(Column = "CREATE_USER")]
        public Guid CreateUserId { get; set; }

        [Property(Column = "CREATE_DTTM")]
        public DateTime? CreateDateTime { get; set; }
        
        [Property(Column = "CHANGE_USER")]
        public Guid? ChangeUserId { get; set; }
        
        [Property(Column = "CHANGE_DTTM")]
        public DateTime? ChangeDateTime { get; set; }

        static BaseDomainObject()
        {
            PersistanceModule pModule = new PersistanceModule();
        }

        public int Save(Guid userId)
        {
            IIdentifiable identifiableObject = null;

            try
            {
                identifiableObject = this as IIdentifiable;
            }
            catch
            {
                throw new Exception("Cannot save an object that does not implement IIdentifiable");
            }

            if ( identifiableObject != null )
            {
                if ( identifiableObject.Id != 0 )
                {
                    this.ChangeDateTime = DateTime.Now;
                    this.ChangeUserId = userId;
                    base.Save();
                }
                else
                {
                    this.CreateDateTime = DateTime.Now;
                    this.CreateUserId = userId;
                    this.Create();
                }
            }

            return identifiableObject.Id;
        }

     
        public bool IsNew
        {
            get
            {
                bool isNew = false;

                IIdentifiable identifiableObject = null;
                try
                {
                    identifiableObject = this as IIdentifiable;
                }
                catch
                {
                    throw new Exception("Cannot get an object that does not implement IIdentifiable");
                }

                if (identifiableObject != null)
                {
                   isNew = identifiableObject.Id == 0 ? true : false;
                }

                return isNew;
            }
        }

    }
}
