using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Marimba
{
    class Member
    {
        public string firstName;
        public string lastName;
        public string otherInstrument;
        public string email;
        public string comments;
        public Instrument curInstrument;
        public bool[] playsInstrument;
        public MemberType type;
        public Faculty memberFaculty;
        public uint uiStudentNumber;
        public short sID;
        public DateTime signupTime;
        public ShirtSize size;
        public bool bMultipleInstruments;

        public enum MemberType { UWUnderGrad = 0, UWGrad = 1, UWAlumni = 2, Other = 3 };
        public enum Faculty { AHS, Arts, Engineering, Environment, Mathematics, Science, Unknown = -1 };
        public enum ShirtSize { XS, S, M, L, XL, XXL, Unknown = -1 };
        public enum Instrument { Piccolo, Flute, Oboe, Bassoon, EbClarinet, Clarinet, AltoClarinet, BassClarinet, SopranoSax, AltoSax, TenorSax, BariSax, Trumpet, Cornet, Horn,
        Trombone, BassTrombone, Euphonium, Tuba, StringBass, ElectricBass, Percussion, DrumKit, Timpani, Mallet, Piano, Baton, Other }

        public Member(string firstName, string lastName, MemberType type, uint uiStudentNumber, int iFaculty, string instrument, string otherInstrument, string email, string comments, int iShirt)
        {
            //declare basic information about user
            this.firstName = firstName;
            this.lastName = lastName;
            this.type = type;
            this.uiStudentNumber = uiStudentNumber;
            this.memberFaculty = (Faculty)iFaculty;
            this.curInstrument = stringToInstrument(instrument);
            if (this.curInstrument == Instrument.Other && String.IsNullOrEmpty(otherInstrument))
                this.otherInstrument = instrument;
            else
                this.otherInstrument = otherInstrument;
            this.email = email;
            this.comments = comments;
            this.sID = clsStorage.currentClub.iMember;
            this.signupTime = DateTime.Now;
            this.size = (ShirtSize)iShirt;
            this.bMultipleInstruments = false;
        }

        public Member(StreamReader sr)
        {
            firstName = sr.ReadLine();
            lastName = sr.ReadLine();
            type = (MemberType)Convert.ToInt32(sr.ReadLine());
            uiStudentNumber = Convert.ToUInt32(sr.ReadLine());
            memberFaculty = (Faculty)Convert.ToInt32(sr.ReadLine());

            otherInstrument = sr.ReadLine();
            curInstrument = (Instrument)Convert.ToInt32(sr.ReadLine());

            bMultipleInstruments = Convert.ToBoolean(sr.ReadLine());

            //write if the member plays multiple instruments
            //write any other instruments that the member plays (or does not play)
            int numberOfInstruments = Enum.GetValues(typeof(Member.Instrument)).Length;
            if (bMultipleInstruments)
            {
                playsInstrument = new bool[Enum.GetValues(typeof(Member.Instrument)).Length];
                for (int j = 0; j < numberOfInstruments; j++)
                    playsInstrument[j] = Convert.ToBoolean(sr.ReadLine());
            }

            email = sr.ReadLine();
            comments = clsStorage.reverseCleanNewLine(sr.ReadLine());
            sID = Convert.ToInt16(sr.ReadLine());
            signupTime = new DateTime(Convert.ToInt64(sr.ReadLine()));
            size = (ShirtSize)Convert.ToInt32(sr.ReadLine());
        }

        public Member(string firstName, string lastName, MemberType type, uint uiStudentNumber, int iFaculty, string instrument, string otherInstrument, string email, string comment, int iShirt, bool[] bMultiple)
        {
            //declare basic information about user
            this.firstName = firstName;
            this.lastName = lastName;
            this.type = type;
            this.uiStudentNumber = uiStudentNumber;
            this.memberFaculty = (Faculty)iFaculty;
            this.curInstrument = stringToInstrument(instrument);
            if (this.curInstrument == Instrument.Other && String.IsNullOrEmpty(otherInstrument))
                this.otherInstrument = instrument;
            else
                this.otherInstrument = otherInstrument;
            this.email = email;
            this.comments = comment;
            this.sID = clsStorage.currentClub.iMember;
            this.signupTime = DateTime.Now;
            this.size = (ShirtSize)iShirt;
            //handle the multiple instruments here
            this.bMultipleInstruments = true;
            this.playsInstrument = new bool[Enum.GetValues(typeof(Member.Instrument)).Length];
            Array.Copy(bMultiple, this.playsInstrument, Enum.GetValues(typeof(Member.Instrument)).Length);
        }
        public Member(string firstName, string lastName, Member.MemberType type, uint uiStudentNumber, int iFaculty, string instrument, string email,
            string comment, short clubID, DateTime time, int iShirt, bool[] bMultiple = null)
        {
            //declare basic information about user
            this.firstName = firstName;
            this.lastName = lastName;
            this.type = type;
            this.uiStudentNumber = uiStudentNumber;
            this.memberFaculty = (Faculty)iFaculty;

            //this is legacy support
            //let's bring it to the Marimba 2 standards
            //take the members instrument, try to recognize it
            //if we fail, mark it as other
            this.curInstrument = stringToInstrument(instrument);
            if (this.curInstrument == Instrument.Other)
                this.otherInstrument = instrument;
            else
                this.otherInstrument = "";

            this.email = email;
            this.comments = comment;
            this.sID = clubID;
            this.signupTime = time;
            this.size = (ShirtSize)iShirt;
            //multiple instruments
            if (bMultiple == null)
                this.bMultipleInstruments = false;
            else
            {
                this.bMultipleInstruments = true;
                this.playsInstrument = new bool[Enum.GetValues(typeof(Member.Instrument)).Length];
                Array.Copy(bMultiple, this.playsInstrument, Enum.GetValues(typeof(Member.Instrument)).Length);
            }
        }
        public Member(string firstName, string lastName, Member.MemberType type, uint uiStudentNumber, int iFaculty, string instrument, string email, string comment, DateTime time, int iShirt)
        {
            //declare basic information about user
            this.firstName = firstName;
            this.lastName = lastName;
            this.type = type;
            this.uiStudentNumber = uiStudentNumber;
            this.memberFaculty = (Faculty)iFaculty;
            this.otherInstrument = instrument;
            this.curInstrument = stringToInstrument(instrument);
            this.email = email;
            this.comments = comment;
            this.sID = clsStorage.currentClub.iMember;
            this.signupTime = time;
            this.size = (ShirtSize)iShirt;
        }

        /// <summary>
        /// Edit the details of a member
        /// </summary>
        /// <param name="strFName">First name</param>
        /// <param name="strLName">Last name</param>
        /// <param name="type">Student/Alumni/Faculty</param>
        /// <param name="uiStudentNumber">Student Number</param>
        /// <param name="iFaculty">Faculty</param>
        /// <param name="strInstrument">Instrument</param>
        /// <param name="strEmail">Email</param>
        /// <param name="strOther">Other info</param>
        /// <param name="time">Signup time</param>
        /// <param name="iShirt">Shirt size</param>
        public void editMember(string strFName, string strLName, MemberType type, uint uiStudentNumber, int iFaculty, string strInstrument, string strEmail,
    string strOther, DateTime time, int iShirt)
        {
            this.firstName = strFName;
            this.lastName = strLName;
            this.type = type;
            this.uiStudentNumber = uiStudentNumber;
            this.memberFaculty = (Faculty)iFaculty;
            this.otherInstrument = "";
            this.curInstrument = stringToInstrument(strInstrument);
            this.email = strEmail;
            this.comments = strOther;
            this.signupTime = time;
            this.size = (ShirtSize)iShirt;
        }

        /// <summary>
        /// Edit the details of a member
        /// </summary>
        /// <param name="strFName">First name</param>
        /// <param name="strLName">Last name</param>
        /// <param name="type">Student/Alumni/Faculty</param>
        /// <param name="uiStudentNumber">Student Number</param>
        /// <param name="iFaculty">Faculty</param>
        /// <param name="strInstrument">Instrument</param>
        /// <param name="strOtherInstrument">Other Instrument</param>
        /// <param name="strEmail">Email</param>
        /// <param name="strOther">Other info</param>
        /// <param name="time">Signup time</param>
        /// <param name="iShirt">Shirt size</param>
        public void editMember(string strFName, string strLName, MemberType type, uint uiStudentNumber, int iFaculty, string strInstrument, string strOtherInstrument, string strEmail,
    string strOther, DateTime time, int iShirt)
        {
            this.firstName = strFName;
            this.lastName = strLName;
            this.type = type;
            this.uiStudentNumber = uiStudentNumber;
            this.memberFaculty = (Faculty)iFaculty;
            this.otherInstrument = strOtherInstrument;
            this.curInstrument = stringToInstrument(strInstrument);
            this.email = strEmail;
            this.comments = strOther;
            this.signupTime = time;
            this.size = (ShirtSize)iShirt;
        }

        /// <summary>
        /// Returns the index of the last common member between two memberlists
        /// </summary>
        /// <param name="low">Low index</param>
        /// <param name="high">High index</param>
        /// <param name="memberlist1">First array of members</param>
        /// <param name="memberlist2">Second array of members</param>
        /// <returns>Returns index of last common member</returns>
        public static int lastCommonMember(int low, int high, Member[] memberlist1, Member[] memberlist2)
        {
            if (low >= high) //found the first differing member
            {
                if (memberlist1[low].email == memberlist2[low].email)
                    return low;
                else
                    return low - 1;
            }
            else
            {
                int mid = (low + high) / 2;
                if (memberlist1[low].email == memberlist2[low].email && memberlist1[mid].email == memberlist2[mid].email)
                    return lastCommonMember(mid + 1, high, memberlist1, memberlist2);
                else
                    return lastCommonMember(low, mid - 1, memberlist1, memberlist2);
            }
        }

        /// <summary>
        /// Export member to a list that can be used for many purposes
        /// </summary>
        /// <returns>List of member's properties</returns>
        public List<object> exportMember()
        {
            List<object> output = new List<object>();

            output.Add(firstName);
            output.Add(lastName);
            output.Add((int)type);
            output.Add(uiStudentNumber);
            output.Add((int)memberFaculty);
            //Note to self: Eventually... change this to an int to save some space
            if (curInstrument == Instrument.Other)
                output.Add(otherInstrument);
            else
                output.Add(instrumentToString(curInstrument));
            output.Add(email);
            output.Add(comments);
            output.Add(sID);
            output.Add(signupTime);
            output.Add((int)size);
            output.Add(bMultipleInstruments);
            if (bMultipleInstruments)
                output.Add(playsInstrument);
            return output;
        }

        public static string toString(MemberType type)
        {
            switch(type)
            {
                case(MemberType.UWUnderGrad):
                    return "UW Undergrad Student";
                case(MemberType.UWGrad):
                    return "UW Grad Student";
                case(MemberType.UWAlumni):
                    return "UW Alumni";
                case(MemberType.Other):
                    return "Other";
            }
            return "Unknown";
        }

        public static string toString(Faculty fac)
        {
            switch (fac)
            {
                case (Faculty.AHS):
                    return "Applied Health Science";
                case (Faculty.Arts):
                    return "Arts";
                case (Faculty.Engineering):
                    return "Engineering";
                case (Faculty.Environment):
                    return "Environment";
                case (Faculty.Mathematics):
                    return "Mathematics";
                case(Faculty.Science):
                    return "Science";
            }
            return "Unknown";
        }

        public static int stringToFaculty(string strFaculty)
        {
            switch (strFaculty.ToLower())
            {
                case ("applied health science"):
                    return (int)Faculty.AHS;
                case ("ahs"):
                    return (int)Faculty.AHS;
                case ("arts"):
                    return (int)Faculty.Arts;
                case ("engineering"):
                    return (int)Faculty.Engineering;
                case ("environment"):
                    return (int)Faculty.Environment;
                case ("mathematics"):
                    return (int)Faculty.Mathematics;
                case ("math"):
                    return (int)Faculty.Mathematics;
                case ("science"):
                    return (int)Faculty.Science;
            }
            //we didn't find anything, so return -1 otherwise
            return -1;
        }

        /// <summary>
        /// Given an instrument, returns its icon index
        /// </summary>
        /// <param name="input">Name of instrument</param>
        /// <returns>Icon index of instrument. Returns 15 (music stand) if not found.</returns>
        public static int instrumentIconIndex(Instrument input)
        {
            switch (input)
            {
                case (Instrument.AltoSax):
                    return 0;
                case (Instrument.AltoClarinet):
                    return 1;
                case (Instrument.BariSax):
                    return 2;
                case (Instrument.BassClarinet):
                    return 3;
                case (Instrument.Bassoon):
                    return 4;
                case (Instrument.Baton):
                    return 5;
                case (Instrument.Cornet):
                    return 23;
                case (Instrument.Clarinet):
                    return 7;
                case (Instrument.EbClarinet):
                    return 7;
                case (Instrument.StringBass):
                    return 8;
                case (Instrument.ElectricBass):
                    return 24;
                case (Instrument.DrumKit):
                    return 9;
                case (Instrument.Percussion):
                    return 10;
                case (Instrument.Euphonium):
                    return 11;
                case (Instrument.Flute):
                    return 12;
                case (Instrument.Horn):
                    return 13;
                case (Instrument.Mallet):
                    return 14;
                case (Instrument.Oboe):
                    return 16;
                case (Instrument.Piano):
                    return 17;
                case (Instrument.Piccolo):
                    return 18;
                case (Instrument.SopranoSax):
                    return 19;
                case (Instrument.TenorSax):
                    return 20;
                case (Instrument.Timpani):
                    return 21;
                case (Instrument.Trombone):
                    return 22;
                case (Instrument.BassTrombone):
                    return 22;
                case (Instrument.Trumpet):
                    return 23;
                case (Instrument.Tuba):
                    return 11;
            }
            //for any other instrument, just return the music stand
            return 15;
        }


        /// <summary>
        /// Given an instrument string, returns its instrument class
        /// </summary>
        /// <param name="strInstrument">Name of instrument</param>
        /// <returns>Icon index of instrument. Returns 15 (music stand) if not found.</returns>
        public static Instrument stringToInstrument(string strInstrument)
        {
            if (String.IsNullOrEmpty(strInstrument))
                return Instrument.Other;
            strInstrument = strInstrument.ToLower();
            switch (strInstrument)
            {
                case ("alto saxophone"):
                    return Instrument.AltoSax;
                case ("alto sax"):
                    return Instrument.AltoSax;
                case ("alto clarinet"):
                    return Instrument.AltoClarinet;
                case ("baritone saxophone"):
                    return Instrument.BariSax;
                case ("baritone sax"):
                    return Instrument.BariSax;;
                case ("bari sax"):
                    return Instrument.BariSax;
                case ("bass clarinet"):
                    return Instrument.BassClarinet;
                case ("bassoon"):
                    return Instrument.Bassoon;
                case ("baton"):
                    return Instrument.Baton;
                case ("cornet"):
                    return Instrument.Cornet;
                case("eb clarinet"):
                    return Instrument.EbClarinet;
                case("e flat clarinet"):
                    return Instrument.EbClarinet;
                case ("guitar"):
                    return Instrument.Other;
                case ("clarinet"):
                    return Instrument.Clarinet;
                case ("bass"):
                    return Instrument.StringBass;
                case ("electric bass"):
                    return Instrument.ElectricBass;
                case ("string bass"):
                    return Instrument.StringBass;
                case ("bass guitar"):
                    return Instrument.ElectricBass;
                case ("drum kit"):
                    return Instrument.DrumKit;
                case ("percussion"):
                    return Instrument.Percussion;
                case ("euphonium"):
                    return Instrument.Euphonium;
                case ("flute"):
                    return Instrument.Flute;
                case ("horn"):
                    return Instrument.Horn;
                case ("french horn"):
                    return Instrument.Horn;
                case ("mallet percussion"):
                    return Instrument.Mallet;
                case ("oboe"):
                    return Instrument.Oboe;
                case ("piano"):
                    return Instrument.Piano;
                case ("piccolo"):
                    return Instrument.Piccolo;
                case ("soprano saxophone"):
                    return Instrument.SopranoSax;
                case ("soprano sax"):
                    return Instrument.SopranoSax;
                case ("tenor saxophone"):
                    return Instrument.TenorSax;
                case ("tenor sax"):
                    return Instrument.TenorSax;
                case ("timpani"):
                    return Instrument.Timpani;
                case ("trombone"):
                    return Instrument.Trombone;
                case ("bass trombone"):
                    return Instrument.BassTrombone;
                case ("trumpet"):
                    return Instrument.Trumpet;
                case ("tuba"):
                    return Instrument.Tuba;
            }
            return Instrument.Other;
        }

        
        /// <summary>
        /// Given an instrument, returns its icon index
        /// </summary>
        /// <param name="input">Name of instrument</param>
        /// <returns>Icon index of instrument. Returns 15 (music stand) if not found.</returns>
        public static string instrumentToString(Instrument input)
        {
            switch (input)
            {
                case (Instrument.AltoSax):
                    return "Alto Saxophone";
                case (Instrument.AltoClarinet):
                    return "Alto Clarinet";
                case (Instrument.BariSax):
                    return "Baritone Saxophone";
                case (Instrument.BassClarinet):
                    return "Bass Clarinet";
                case (Instrument.Bassoon):
                    return "Bassoon";
                case (Instrument.Baton):
                    return "Baton";
                case (Instrument.Cornet):
                    return "Cornet";
                case (Instrument.Clarinet):
                    return "Clarinet";
                case (Instrument.EbClarinet):
                    return "Eb Clarinet";
                case (Instrument.StringBass):
                    return "String Bass";
                case (Instrument.ElectricBass):
                    return "Electric Bass";
                case (Instrument.DrumKit):
                    return "Drum Kit";
                case (Instrument.Percussion):
                    return "Percussion";
                case (Instrument.Euphonium):
                    return "Euphonium";
                case (Instrument.Flute):
                    return "Flute";
                case (Instrument.Horn):
                    return "French Horn";
                case (Instrument.Mallet):
                    return "Mallet Percussion";
                case (Instrument.Oboe):
                    return "Oboe";
                case (Instrument.Piano):
                    return "Piano";
                case (Instrument.Piccolo):
                    return "Piccolo";
                case (Instrument.SopranoSax):
                    return "Soprano Saxophone";
                case (Instrument.TenorSax):
                    return "Tenor Saxophone";
                case (Instrument.Timpani):
                    return "Timpani";
                case (Instrument.Trombone):
                    return "Trombone";
                case (Instrument.BassTrombone):
                    return "Bass Trombone";
                case (Instrument.Trumpet):
                    return "Trumpet";
                case (Instrument.Tuba):
                    return "Tuba";
            }
            //for any other instrument, just return the music stand
            return "Other";
        }
    }
}
