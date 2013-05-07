using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Windows.Forms;

namespace CS_WinForms
{
        class MyWebClient : WebClient
        {
            Uri _responseUri;

            public Uri ResponseUri
            {
                get { return _responseUri; }
            }

            protected override WebResponse GetWebResponse(WebRequest request)
            {

                    WebResponse response = null;
                    response = base.GetWebResponse(request);
                    _responseUri = response.ResponseUri;
                    return response;


            }
        }
    }

