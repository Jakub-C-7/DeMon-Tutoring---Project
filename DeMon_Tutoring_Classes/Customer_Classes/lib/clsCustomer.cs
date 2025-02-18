﻿using DeMon_Tutoring_Classes.Staffing_Classes.lib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeMon_Tutoring_Classes.Customer_Classes.lib
{
    public class ClsCustomer
    {
        // Customer Fields
        // public Boolean Active { get; set; }
        // private Boolean active { get; set; }
        private Int32 mCustomerID;
        public Int32 CustomerID
        { get { return mCustomerID; } set { mCustomerID = value; } }

        private Boolean mCustomer;
        public Boolean Customer
        { get { return mCustomer; } set { mCustomer = value; } }

        private Name mCustomerName;
        public Name CustomerName
        { get { return mCustomerName; } set { mCustomerName = value; } }

        private DateTime mDateOfBirth;
        public DateTime DateOfBirth
        { get { return mDateOfBirth; } set { mDateOfBirth = value; } }

        private string mEmail;
        public string Email
        { get { return mEmail; } set { mEmail = value; } }
  
        private string mPhoneNumber;
        public string PhoneNumber
        { get { return mPhoneNumber; } set { mPhoneNumber = value; } }

        private string mPassword;
        public string Password
        { get { return mPassword; } set { mPassword = value; } }

        private string mCardNo;
        public string CardNo
        { get { return mCardNo; } set { mCardNo = value; } }
        
        private DateTime mCardDate;
        public DateTime CardDate
        { get { return mCardDate; } set { mCardDate = value; } }


        //Customer Constructors
        public ClsCustomer(int cID, Name cName, DateTime cDob, string cEmail, string cNumber, string cPword, string cCardNo, DateTime cCardDate)
        {
            CustomerID = cID;
            CustomerName = cName;
            DateOfBirth = cDob;
            Email = cEmail;
            PhoneNumber = cNumber;
            Password = cPword;
            CardNo = cCardNo;
            CardDate = cCardDate;

        }

        public ClsCustomer()
        {
            CustomerID = 1;
            CustomerName = null;
            DateOfBirth = DateTime.Today;
            Email = " ";
            PhoneNumber = " ";
            Password = " ";
            CardNo = " ";
            CardDate = DateTime.Today;        
        }

        //methods

        //ToString Method
        public string toString()
        {
            return "CustomerID: " + this.CustomerID + ", CustomerName: " + this.CustomerName +
                ", DateOfBirth: " + this.DateOfBirth + ", Email: " + this.Email + ", PhoneNumber: " + this.PhoneNumber +
                ", Password: " + this.Password + ", CardNo: " + this.CardNo + ", CardDate: " + this.CardDate;
        }

        //the Find method to find a customer in the database
        public bool Find (int CustomerID)
        {
            bool Found = false;
            //creating an instance of the data connection
            clsDataConnection DB = new clsDataConnection();
            //adding a parameter for the customer id to search for
            DB.AddParameter("@CustomerID", CustomerID);
            //execute stored procedure
            DB.Execute("sproc_tblCustomer_FilterByCustomerID");
            //when a record is found, should be one or none
            if (DB.Count == 1)
            {
                //copies all data from database to the private data memebers in this class
                mCustomerID = Convert.ToInt32(DB.DataTable.Rows[0]["CustomerID"]); 
                mCustomerName = new Name(Convert.ToString(DB.DataTable.Rows[0]["FirstName"]), Convert.ToString(DB.DataTable.Rows[0]["LastName"]));
                mDateOfBirth = Convert.ToDateTime(DB.DataTable.Rows[0]["DateOfBirth"]);
                mEmail = Convert.ToString(DB.DataTable.Rows[0]["Email"]);
                mPhoneNumber = Convert.ToString(DB.DataTable.Rows[0]["PhoneNumber"]);
                mPassword = Convert.ToString(DB.DataTable.Rows[0]["Password"]);
                mCardNo = Convert.ToString(DB.DataTable.Rows[0]["CardNo"]);
                mCardDate = Convert.ToDateTime(DB.DataTable.Rows[0]["CardDate"]);

                //returns all info on the customer
                Found = true;

            }
            return Found;
        }

        public string Valid(string cFirstName, string cLastName, string cDateOfBirth, string cEmail, string cPhoneNumber, string cPassword, string cCardNo, string cCardDate)
        {
            //create string variable to store the error 
            String Error = "";
            //create a temporary variable to strore data values
            DateTime DateTemp;
            DateTime DateTemp2;


            //if the first name is empty
            if (cFirstName.Length == 0)
            {
                //record the error
                Error = Error + "First name cannot be blank";
            }
            //if first name is more than 25 characters
            if (cFirstName.Length > 25)
            {
                //recrd the error
                Error = Error + "First name cannot be more than 25 characters";

            }
            //if last name is empty
            if (cLastName.Length == 0)
            {
                //record the error
                Error = Error + "Last name cannot be blank ";
            }
            //if the last name is more than 25 characters 
            if (cLastName.Length > 25)
            {
                //record the error
                Error = Error + "Last name cannot be more than 25 characters";
            }
            //if the date of birth is less that 16 years
      
            try
            {
                //copy the dateAdded value to the DateTemp variable
                DateTemp = Convert.ToDateTime(cDateOfBirth);
                //check to see if date is in the past
                if (DateTemp > DateTime.Now.Date.AddYears(-16))
                {
                    //record the error
                    Error = Error + "Cannot be under 16 years old";
                }
                if (DateTemp > DateTime.Now.Date)
                {
                    //record the error
                    Error = Error + "date of birth cannot be in the future : ";
                }
            }
            catch
            {
                //record the error
                Error = Error + "This date of birth is not valid: ";
            }
            //if email is less than 5 it will throw error
            if (cEmail.Length < 5)
            {
                //record the error
                Error = Error + "Email cannot be less than 5 characters";
            }
            //if the email is more than 50 characters 
            if (cEmail.Length > 50)
            {
                //record the error
                Error = Error + "Email cannot be more than 50 characters";
            }
            //if phone number is not 11 characters
            if (cPhoneNumber.Length != 11)
            {
                //record the error
                Error = Error + "Phone number must be 11 digits";
            }
            //if password is blank
            if (cPassword.Length == 0)
            {
                //record the error
                Error = Error + "Password cannot be blank";
            }
            //if password is greater than 25
            if (cPassword.Length > 25)
            {
                //records the error
                Error = Error + "Password cannot be more than 25 characters";
            }
            if (cCardNo.Length != 16)
            {
                //record the error
                Error = Error + "Card number must be 16 digits";
            }

            try
            {
                //if the card date is more than 5 years in the future
                DateTemp2 = Convert.ToDateTime(cCardDate);
                if (DateTemp2 > DateTime.Now.Date.AddYears(+5))
                {
                    //record the error
                    Error = Error + "Invalid card date";
                }
                //if the card date if in the past it has expired
                DateTemp2 = Convert.ToDateTime(cCardDate);
                if (DateTemp2 < DateTime.Now.Date)
                {
                    //record the error
                    Error = Error + "This card has expired";
                }
            }
            catch
            {
                //record the error
                Error = Error + "The date is not a valid date: ";
            }
            return Error;
        }
       
    }
}


    

