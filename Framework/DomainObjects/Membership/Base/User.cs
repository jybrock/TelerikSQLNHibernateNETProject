using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainObjects
{
    public class User
    {
        private Guid _UserID = new Guid();
        private string _UserName;
        private string _Password;
        private string _FirstName;
        private string _LastName;
        private string _PhoneNumber;
        private string _Extension;
        private string _Address;
        private string _City;
        private string _State;
        private string _ZipCode;
        private string _Email;
        private bool _IsApproved;

        public enum EntityType : int
        {
            ResponseRateGoals = 1
        }

        public enum ResultType : int
        {
            String = 1,
            Numeric = 2
        }

        public Guid UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }

        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }

        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; }
        }

        public string Extension
        {
            get { return _Extension; }
            set { _Extension = value; }
        }

        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        public string City
        {
            get { return _City; }
            set { _City = value; }
        }

        public string State
        {
            get { return _State; }
            set { _State = value; }
        }

        public string ZipCode
        {
            get { return _ZipCode; }
            set { _ZipCode = value; }
        }

        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        public bool IsApproved
        {
            get { return _IsApproved; }
            set { _IsApproved = value; }
        }
    }
}
