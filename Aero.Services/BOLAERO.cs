using System;

namespace AERO.Services
{
    class BOLAERO
    {
        public class BOLEmailModel
        {
            Int32 _Id;
            public Int32 Id
            {
                get { return _Id; }
                set { _Id = value; }
            }
            String _ToEmail;
            public String ToEmail
            {
                get { return _ToEmail; }
                set { _ToEmail = value; }
            }
            String _CCEmail;
            public String CCEmail
            {
                get { return _CCEmail; }
                set { _CCEmail = value; }
            }
            String _Subject;
            public String Subject
            {
                get { return _Subject; }
                set { _Subject = value; }
            }
            String _Body;
            public String Body
            {
                get { return _Body; }
                set { _Body = value; }
            }
            String _EmailType;
            public String EmailType
            {
                get { return _EmailType; }
                set { _EmailType = value; }
            }           
            Int32 _Operation;
            public Int32 Operation
            {
                get { return _Operation; }
                set { _Operation = value; }
            }
            String _ErrorMessage;
            public String ErrorMessage
            {
                get { return _ErrorMessage; }
                set { _ErrorMessage = value; }
            }
            Int32 _ReferenceId;
            public Int32 ReferenceId
            {
                get { return _ReferenceId; }
                set { _ReferenceId = value; }
            }
        }
        public class BOLException
        {
            Int32 _id;
            public Int32 id
            {
                get { return _id; }
                set { _id = value; }
            }
            Char _Action;
            public Char Action
            {
                get { return _Action; }
                set { _Action = value; }
            }
            String _module_name;
            public String module_name
            {
                get { return _module_name; }
                set { _module_name = value; }
            }
            String _source;
            public String source
            {
                get { return _source; }
                set { _source = value; }
            }
            String _message;
            public String message
            {
                get { return _message; }
                set { _message = value; }
            }
            String _data;
            public String data
            {
                get { return _data; }
                set { _data = value; }
            }
            String _target_site;
            public String target_site
            {
                get { return _target_site; }
                set { _target_site = value; }
            }
            String _stack_trace;
            public String stack_trace
            {
                get { return _stack_trace; }
                set { _stack_trace = value; }
            }
            DateTime _date;
            public DateTime date
            {
                get { return _date; }
                set { _date = value; }
            }
            Int32 _counts;
            public Int32 counts
            {
                get { return _counts; }
                set { _counts = value; }
            }
            String _ErrorMessage;
            public String ErrorMessage
            {
                get { return _ErrorMessage; }
                set { _ErrorMessage = value; }
            }

        }
    }
}
