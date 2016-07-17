using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Marimba
{
    class member
    {
        public string strFName, strLName,strOtherInstrument, strEmail, strOther;
        public instrument curInstrument;
        public bool[] playsInstrument;
        public membertype type;
        public faculty memberFaculty;
        public uint uiStudentNumber;
        public short sID;
        public DateTime signupTime;
        public shirtSize size;
        public bool bMultipleInstruments;

        public enum membertype { UWUnderGrad, UWGrad, UWAlumni, Other };
        public enum faculty { AHS, Arts, Engineering, Environment, Mathematics, Science, Unknown = -1 };
        public enum shirtSize { XS, S, M, L, XL, XXL, Unknown = -1 };
        public enum instrument { piccolo, flute, oboe,bassoon, eb_clarinet, clarinet, alto_clarinet, bass_clarinet, sop_sax, alto_sax, tenor_sax, bari_sax, trumpet, cornet, horn,
        trombone, bass_trombone, euph, tuba, string_bass, elec_bass, percussion, drum_kit, timpani, mallet, piano, baton, other}
        public member(string strFName, string strLName, int iType, uint uiStudentNumber, int iFaculty, string strInstrument, string strOtherInstrument, string strEmail, string strOther, int iShirt)
        {
            //declare basic information about user
            this.strFName = strFName;
            this.strLName = strLName;
            this.type = (membertype)iType;
            this.uiStudentNumber = uiStudentNumber;
            this.memberFaculty = (faculty)iFaculty;
            this.curInstrument = stringToInstrument(strInstrument);
            if (this.curInstrument == instrument.other && String.IsNullOrEmpty(strOtherInstrument))
                this.strOtherInstrument = strInstrument;
            else
                this.strOtherInstrument = strOtherInstrument;
            this.strEmail = strEmail;
            this.strOther = strOther;
            this.sID = clsStorage.currentClub.iMember;
            this.signupTime = DateTime.Now;
            this.size = (shirtSize)iShirt;
            this.bMultipleInstruments = false;
        }

        public member(StreamReader sr)
        {
            strFName = sr.ReadLine();
            strLName = sr.ReadLine();
            type = (membertype)Convert.ToInt32(sr.ReadLine());
            uiStudentNumber = Convert.ToUInt32(sr.ReadLine());
            memberFaculty = (faculty)Convert.ToInt32(sr.ReadLine());

            strOtherInstrument = sr.ReadLine();
            curInstrument = (instrument)Convert.ToInt32(sr.ReadLine());

            bMultipleInstruments = Convert.ToBoolean(sr.ReadLine());

            //write if the member plays multiple instruments
            //write any other instruments that the member plays (or does not play)
            int numberOfInstruments = Enum.GetValues(typeof(member.instrument)).Length;
            if (bMultipleInstruments)
            {
                playsInstrument = new bool[Enum.GetValues(typeof(member.instrument)).Length];
                for (int j = 0; j < numberOfInstruments; j++)
                    playsInstrument[j] = Convert.ToBoolean(sr.ReadLine());
            }

            strEmail = sr.ReadLine();
            strOther = clsStorage.reverseCleanNewLine(sr.ReadLine());
            sID = Convert.ToInt16(sr.ReadLine());
            signupTime = new DateTime(Convert.ToInt64(sr.ReadLine()));
            size = (shirtSize)Convert.ToInt32(sr.ReadLine());
        }

        public member(string strFName, string strLName, int iType, uint uiStudentNumber, int iFaculty, string strInstrument, string strOtherInstrument, string strEmail, string strOther, int iShirt, bool[] bMultiple)
        {
            //declare basic information about user
            this.strFName = strFName;
            this.strLName = strLName;
            this.type = (membertype)iType;
            this.uiStudentNumber = uiStudentNumber;
            this.memberFaculty = (faculty)iFaculty;
            this.curInstrument = stringToInstrument(strInstrument);
            if (this.curInstrument == instrument.other && String.IsNullOrEmpty(strOtherInstrument))
                this.strOtherInstrument = strInstrument;
            else
                this.strOtherInstrument = strOtherInstrument;
            this.strEmail = strEmail;
            this.strOther = strOther;
            this.sID = clsStorage.currentClub.iMember;
            this.signupTime = DateTime.Now;
            this.size = (shirtSize)iShirt;
            //handle the multiple instruments here
            this.bMultipleInstruments = true;
            this.playsInstrument = new bool[Enum.GetValues(typeof(member.instrument)).Length];
            Array.Copy(bMultiple, this.playsInstrument, Enum.GetValues(typeof(member.instrument)).Length);
        }
        public member(string strFName, string strLName, int iType, uint uiStudentNumber, int iFaculty, string strInstrument, string strEmail,
            string strOther, short clubID, DateTime time, int iShirt, bool[] bMultiple = null)
        {
            //declare basic information about user
            this.strFName = strFName;
            this.strLName = strLName;
            this.type = (membertype)iType;
            this.uiStudentNumber = uiStudentNumber;
            this.memberFaculty = (faculty)iFaculty;

            //this is legacy support
            //let's bring it to the Marimba 2 standards
            //take the members instrument, try to recognize it
            //if we fail, mark it as other
            this.curInstrument = stringToInstrument(strInstrument);
            if (this.curInstrument == instrument.other)
                this.strOtherInstrument = strInstrument;
            else
                this.strOtherInstrument = "";

            this.strEmail = strEmail;
            this.strOther = strOther;
            this.sID = clubID;
            this.signupTime = time;
            this.size = (shirtSize)iShirt;
            //multiple instruments
            if (bMultiple == null)
                this.bMultipleInstruments = false;
            else
            {
                this.bMultipleInstruments = true;
                this.playsInstrument = new bool[Enum.GetValues(typeof(member.instrument)).Length];
                Array.Copy(bMultiple, this.playsInstrument, Enum.GetValues(typeof(member.instrument)).Length);
            }
        }
        public member(string strFName, string strLName, int iType, uint uiStudentNumber, int iFaculty, string strInstrument, string strEmail,
    string strOther, DateTime time, int iShirt)
        {
            //declare basic information about user
            this.strFName = strFName;
            this.strLName = strLName;
            this.type = (membertype)iType;
            this.uiStudentNumber = uiStudentNumber;
            this.memberFaculty = (faculty)iFaculty;
            this.strOtherInstrument = strInstrument;
            this.curInstrument = stringToInstrument(strInstrument);
            this.strEmail = strEmail;
            this.strOther = strOther;
            this.sID = clsStorage.currentClub.iMember;
            this.signupTime = time;
            this.size = (shirtSize)iShirt;
        }

        /// <summary>
        /// Edit the details of a member
        /// </summary>
        /// <param name="strFName">First name</param>
        /// <param name="strLName">Last name</param>
        /// <param name="iType">Student/Alumni/Faculty</param>
        /// <param name="uiStudentNumber">Student Number</param>
        /// <param name="iFaculty">Faculty</param>
        /// <param name="strInstrument">Instrument</param>
        /// <param name="strEmail">Email</param>
        /// <param name="strOther">Other info</param>
        /// <param name="time">Signup time</param>
        /// <param name="iShirt">Shirt size</param>
        public void editMember(string strFName, string strLName, int iType, uint uiStudentNumber, int iFaculty, string strInstrument, string strEmail,
    string strOther, DateTime time, int iShirt)
        {
            this.strFName = strFName;
            this.strLName = strLName;
            this.type = (membertype)iType;
            this.uiStudentNumber = uiStudentNumber;
            this.memberFaculty = (faculty)iFaculty;
            this.strOtherInstrument = "";
            this.curInstrument = stringToInstrument(strInstrument);
            this.strEmail = strEmail;
            this.strOther = strOther;
            this.signupTime = time;
            this.size = (shirtSize)iShirt;
        }

        /// <summary>
        /// Edit the details of a member
        /// </summary>
        /// <param name="strFName">First name</param>
        /// <param name="strLName">Last name</param>
        /// <param name="iType">Student/Alumni/Faculty</param>
        /// <param name="uiStudentNumber">Student Number</param>
        /// <param name="iFaculty">Faculty</param>
        /// <param name="strInstrument">Instrument</param>
        /// <param name="strOtherInstrument">Other Instrument</param>
        /// <param name="strEmail">Email</param>
        /// <param name="strOther">Other info</param>
        /// <param name="time">Signup time</param>
        /// <param name="iShirt">Shirt size</param>
        public void editMember(string strFName, string strLName, int iType, uint uiStudentNumber, int iFaculty, string strInstrument, string strOtherInstrument, string strEmail,
    string strOther, DateTime time, int iShirt)
        {
            this.strFName = strFName;
            this.strLName = strLName;
            this.type = (membertype)iType;
            this.uiStudentNumber = uiStudentNumber;
            this.memberFaculty = (faculty)iFaculty;
            this.strOtherInstrument = strOtherInstrument;
            this.curInstrument = stringToInstrument(strInstrument);
            this.strEmail = strEmail;
            this.strOther = strOther;
            this.signupTime = time;
            this.size = (shirtSize)iShirt;
        }

        /// <summary>
        /// Returns the index of the last common member between two memberlists
        /// </summary>
        /// <param name="low">Low index</param>
        /// <param name="high">High index</param>
        /// <param name="memberlist1">First array of members</param>
        /// <param name="memberlist2">Second array of members</param>
        /// <returns>Returns index of last common member</returns>
        public static int lastCommonMember(int low, int high, member[] memberlist1, member[] memberlist2)
        {
            if (low >= high) //found the first differing member
            {
                if (memberlist1[low].strEmail == memberlist2[low].strEmail)
                    return low;
                else
                    return low - 1;
            }
            else
            {
                int mid = (low + high) / 2;
                if (memberlist1[low].strEmail == memberlist2[low].strEmail && memberlist1[mid].strEmail == memberlist2[mid].strEmail)
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

            output.Add(strFName);
            output.Add(strLName);
            output.Add((int)type);
            output.Add(uiStudentNumber);
            output.Add((int)memberFaculty);
            //Note to self: Eventually... change this to an int to save some space
            if (curInstrument == instrument.other)
                output.Add(strOtherInstrument);
            else
                output.Add(instrumentToString(curInstrument));
            output.Add(strEmail);
            output.Add(strOther);
            output.Add(sID);
            output.Add(signupTime);
            output.Add((int)size);
            output.Add(bMultipleInstruments);
            if (bMultipleInstruments)
                output.Add(playsInstrument);
            return output;
        }

        public static string toString(membertype type)
        {
            switch(type)
            {
                case(membertype.UWUnderGrad):
                    return "UW Undergrad Student";
                case(membertype.UWGrad):
                    return "UW Grad Student";
                case(membertype.UWAlumni):
                    return "UW Alumni";
                case(membertype.Other):
                    return "Other";
            }
            return "Unknown";
        }

        public static string toString(faculty fac)
        {
            switch (fac)
            {
                case (faculty.AHS):
                    return "Applied Health Science";
                case (faculty.Arts):
                    return "Arts";
                case (faculty.Engineering):
                    return "Engineering";
                case (faculty.Environment):
                    return "Environment";
                case (faculty.Mathematics):
                    return "Mathematics";
                case(faculty.Science):
                    return "Science";
            }
            return "Unknown";
        }

        public static int stringToFaculty(string strFaculty)
        {
            switch (strFaculty.ToLower())
            {
                case ("applied health science"):
                    return (int)faculty.AHS;
                case ("ahs"):
                    return (int)faculty.AHS;
                case ("arts"):
                    return (int)faculty.Arts;
                case ("engineering"):
                    return (int)faculty.Engineering;
                case ("environment"):
                    return (int)faculty.Environment;
                case ("mathematics"):
                    return (int)faculty.Mathematics;
                case ("math"):
                    return (int)faculty.Mathematics;
                case ("science"):
                    return (int)faculty.Science;
            }
            //we didn't find anything, so return -1 otherwise
            return -1;
        }

        /// <summary>
        /// Given an instrument, returns its icon index
        /// </summary>
        /// <param name="input">Name of instrument</param>
        /// <returns>Icon index of instrument. Returns 15 (music stand) if not found.</returns>
        public static int instrumentIconIndex(instrument input)
        {
            switch (input)
            {
                case (instrument.alto_sax):
                    return 0;
                case (instrument.alto_clarinet):
                    return 1;
                case (instrument.bari_sax):
                    return 2;
                case (instrument.bass_clarinet):
                    return 3;
                case (instrument.bassoon):
                    return 4;
                case (instrument.baton):
                    return 5;
                case (instrument.cornet):
                    return 23;
                case (instrument.clarinet):
                    return 7;
                case (instrument.eb_clarinet):
                    return 7;
                case (instrument.string_bass):
                    return 8;
                case (instrument.elec_bass):
                    return 24;
                case (instrument.drum_kit):
                    return 9;
                case (instrument.percussion):
                    return 10;
                case (instrument.euph):
                    return 11;
                case (instrument.flute):
                    return 12;
                case (instrument.horn):
                    return 13;
                case (instrument.mallet):
                    return 14;
                case (instrument.oboe):
                    return 16;
                case (instrument.piano):
                    return 17;
                case (instrument.piccolo):
                    return 18;
                case (instrument.sop_sax):
                    return 19;
                case (instrument.tenor_sax):
                    return 20;
                case (instrument.timpani):
                    return 21;
                case (instrument.trombone):
                    return 22;
                case (instrument.bass_trombone):
                    return 22;
                case (instrument.trumpet):
                    return 23;
                case (instrument.tuba):
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
        public static instrument stringToInstrument(string strInstrument)
        {
            if (String.IsNullOrEmpty(strInstrument))
                return instrument.other;
            strInstrument = strInstrument.ToLower();
            switch (strInstrument)
            {
                case ("alto saxophone"):
                    return instrument.alto_sax;
                case ("alto sax"):
                    return instrument.alto_sax;
                case ("alto clarinet"):
                    return instrument.alto_clarinet;
                case ("baritone saxophone"):
                    return instrument.bari_sax;
                case ("baritone sax"):
                    return instrument.bari_sax;;
                case ("bari sax"):
                    return instrument.bari_sax;
                case ("bass clarinet"):
                    return instrument.bass_clarinet;
                case ("bassoon"):
                    return instrument.bassoon;
                case ("baton"):
                    return instrument.baton;
                case ("cornet"):
                    return instrument.cornet;
                case("eb clarinet"):
                    return instrument.eb_clarinet;
                case("e flat clarinet"):
                    return instrument.eb_clarinet;
                case ("guitar"):
                    return instrument.other;
                case ("clarinet"):
                    return instrument.clarinet;
                case ("bass"):
                    return instrument.string_bass;
                case ("electric bass"):
                    return instrument.elec_bass;
                case ("string bass"):
                    return instrument.string_bass;
                case ("bass guitar"):
                    return instrument.elec_bass;
                case ("drum kit"):
                    return instrument.drum_kit;
                case ("percussion"):
                    return instrument.percussion;
                case ("euphonium"):
                    return instrument.euph;
                case ("flute"):
                    return instrument.flute;
                case ("horn"):
                    return instrument.horn;
                case ("french horn"):
                    return instrument.horn;
                case ("mallet percussion"):
                    return instrument.mallet;
                case ("oboe"):
                    return instrument.oboe;
                case ("piano"):
                    return instrument.piano;
                case ("piccolo"):
                    return instrument.piccolo;
                case ("soprano saxophone"):
                    return instrument.sop_sax;
                case ("soprano sax"):
                    return instrument.sop_sax;
                case ("tenor saxophone"):
                    return instrument.tenor_sax;
                case ("tenor sax"):
                    return instrument.tenor_sax;
                case ("timpani"):
                    return instrument.timpani;
                case ("trombone"):
                    return instrument.trombone;
                case ("bass trombone"):
                    return instrument.bass_trombone;
                case ("trumpet"):
                    return instrument.trumpet;
                case ("tuba"):
                    return instrument.tuba;
            }
            return instrument.other;
        }

        
        /// <summary>
        /// Given an instrument, returns its icon index
        /// </summary>
        /// <param name="input">Name of instrument</param>
        /// <returns>Icon index of instrument. Returns 15 (music stand) if not found.</returns>
        public static string instrumentToString(instrument input)
        {
            switch (input)
            {
                case (instrument.alto_sax):
                    return "Alto Saxophone";
                case (instrument.alto_clarinet):
                    return "Alto Clarinet";
                case (instrument.bari_sax):
                    return "Baritone Saxophone";
                case (instrument.bass_clarinet):
                    return "Bass Clarinet";
                case (instrument.bassoon):
                    return "Bassoon";
                case (instrument.baton):
                    return "Baton";
                case (instrument.cornet):
                    return "Cornet";
                case (instrument.clarinet):
                    return "Clarinet";
                case (instrument.eb_clarinet):
                    return "Eb Clarinet";
                case (instrument.string_bass):
                    return "String Bass";
                case (instrument.elec_bass):
                    return "Electric Bass";
                case (instrument.drum_kit):
                    return "Drum Kit";
                case (instrument.percussion):
                    return "Percussion";
                case (instrument.euph):
                    return "Euphonium";
                case (instrument.flute):
                    return "Flute";
                case (instrument.horn):
                    return "French Horn";
                case (instrument.mallet):
                    return "Mallet Percussion";
                case (instrument.oboe):
                    return "Oboe";
                case (instrument.piano):
                    return "Piano";
                case (instrument.piccolo):
                    return "Piccolo";
                case (instrument.sop_sax):
                    return "Soprano Saxophone";
                case (instrument.tenor_sax):
                    return "Tenor Saxophone";
                case (instrument.timpani):
                    return "Timpani";
                case (instrument.trombone):
                    return "Trombone";
                case (instrument.bass_trombone):
                    return "Bass Trombone";
                case (instrument.trumpet):
                    return "Trumpet";
                case (instrument.tuba):
                    return "Tuba";
            }
            //for any other instrument, just return the music stand
            return "Other";
        }
    }
}
