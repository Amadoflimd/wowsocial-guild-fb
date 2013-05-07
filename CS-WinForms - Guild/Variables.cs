using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_WinForms
{
    
    public class Variables
    {

        static string _Realm;
        static string _Guild;
        static string _AppID;
        static string _PageID;
        static string _Character;

        public static string AppID
        {
            get
            {
                return _AppID;
            }
            set
            {
                _AppID = value;
            }
        }

        public static string PageID
        {
            get
            {
                return _PageID;
            }
            set
            {
                _PageID = value;
            }
        }

        public static string Realm
        {
            get
            {
                return _Realm;
            }
            set
            {
                _Realm = value;
            }
        }

        public static string Guild
        {
            get
            {
                return _Guild;
            }
            set
            {
                _Guild = value;
            }
        }

        public static string Character
        {
            get
            {
                return _Character;
            }
            set
            {
                _Character = value;
            }
        }
    }
}
