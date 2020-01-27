using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace marimba_web.Common
{
    public class Marimba
    {
        // keeping names FinanceType and FinanceCategory since they are now in a common file, might
        // want other categories/types in here
        public enum FinanceType
        {
            Asset,
            Depreciation,
            Expense,
            Revenue
        }

        public enum FinanceCategory
        {
            AdminFees,
            Advertising,
            Buttons,
            Clothing,
            CopyrightFees,
            Donations,
            EquipmentRentals,
            FedsSettlement,
            FoodOrBeverages,
            Instruments,
            Maintenance,
            Miscellaneous,
            MusicEquipment,
            Printing,
            RentalSubsidy,
            SheetMusic,
            WaivedMembershipFee
        }

        public enum StudentType
        {
            Undergrad,
            Grad,
            Alumnus,
            Other
        }

        public enum Faculty
        {
            AppliedHealthSciences,
            Arts,
            Engineering,
            Environment,
            Math,
            Science
        }

        // these are taken from current Marimba list of icons
        public enum Instrument
        {
            AltoSax,
            AltoClarinet,
            BaritoneSax,
            BassClarinet,
            Bassoon,
            Clarinet,
            Drums,
            Euphonium,
            Flute,
            Guitar,
            Mallet,
            Oboe,
            Piano,
            Piccolo,
            SopranoSax,
            TenorSax,
            Timpani,
            Trombone,
            Trumpet
        }

        public enum ShirtSize
        {
            XS,
            S, 
            M, 
            L, 
            XL, 
            XXL, 
            None,
            Unknown = -1
        }

        public enum MemberType
        {
            General,
            Exec,
            Admin
        }

        // these are from current Marimba
        public enum ActionType
        {
            Signup = 0,
            Signin = 1,
            EditMember = 2,
            Unsubscribe = 3,
            Deactivate = 4,
            AddUser = 5,
            SentEmail = 6,
            AddFees = 7,
            AddBudget = 8,
            EditBudget = 9,
            DeleteBudget = 10,
            SetupElection = 11,
            NewTerm = 12,
            ImportMembers = 13,
            EditAttendance = 14,
            RemoveFromTerm = 15,
            PurgeMembers = 16,
            EditUser = 17,
            DeleteUser = 18,
            AddToTerm = 19
        }
    }
}
