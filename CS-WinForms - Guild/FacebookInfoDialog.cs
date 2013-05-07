using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Windows.Forms;
using Facebook;
using System.IO;
using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using System.Text;
using Newtonsoft.Json.Linq;
namespace CS_WinForms
{

    // note: this sample include both synchronous and asynchronous versions.
    // for real production applications, it is recommended that only asynchronous version
    // of the FacebookClient methods are called for app responsiveness and better
    // user experience.

    public partial class FacebookInfoDialog : Form
    {
        private string arg2, arg1, time, type, clColor, link = null, pageAccessToken;
        private readonly string _accessToken;
        private DateTime WiteTime, Timenull;
        private List<string> list = new List<string>();
        Stream data;
        //private string PathLog = "C:/";
        public FacebookInfoDialog(string accessToken)
        {
            _accessToken = accessToken;

            InitializeComponent();
        }

        private void FacebookInfoDialog_Load(object sender, EventArgs e)
        {

            GetUserProfilePicture();
            
            GraphApiExample();
            GraphApiAsyncExample();
            GraphApiAsyncDynamicExample();
            GraphApiParametersInPathExample();

            LegacyRestApiAsyncExample();

            FqlAsyncExample();
            FqlMultiQueryAsyncExample();

            BatchRequestExample();
            BatchRequestAsyncExample();
            //FacebookInfoDialog.ActiveForm.Activate();
          
        }

        private void WaitSeconds(double nSecs)
        {
            // Esperar los segundos indicados

            // Crear la cadena para convertir en TimeSpan
            string s = "0.00:01:" + nSecs.ToString().Replace(",", ".");
            TimeSpan ts = TimeSpan.Parse(s);

            // Añadirle la diferencia a la hora actual
            DateTime t1 = DateTime.Now.Add(ts);

            // Esta asignación solo es necesaria 
            // si la comprobación se hace al principio del bucle
            DateTime t2 = DateTime.Now;

            // Mientras no haya pasado el tiempo indicado
            while (t2 < t1)
            {
                // Un respiro para el sitema
                System.Windows.Forms.Application.DoEvents();
                // Asignar la hora actual
                t2 = DateTime.Now;
            }
        }

        private bool Tiempo(int secs)
        {
            DateTime d1 = default(DateTime);
            DateTime d2 = default(DateTime);

            double d = 0;
            double[] segundos = { secs };


            for (int i = 0; i <= segundos.Length - 1; i++)
            {
                d = segundos[i];

                
                //Console.WriteLine("Esperando... {0} segundos", d)
                d1 = DateTime.Now;
                WaitSeconds(d);
                d2 = DateTime.Now;
                //Console.WriteLine("Inicio: {0}", d1.ToString("HH:mm:ss.ff"))
                //Console.WriteLine("final : {0}", d2.ToString("HH:mm:ss.ff"))
                //Console.WriteLine("               {0}", (d2 - d1).TotalSeconds.ToString("0.00"))

                //Console.WriteLine()
            }
            return true;


        }


        private void GetUserProfilePicture()
        {
            // note: avoid using synchronous methods if possible as it will block the thread until the result is receive

            try
            {
                var fb = new FacebookClient(_accessToken);

                // Note: the result can either me IDictionary<string,object> or IList<object>
                // json objects with properties can be casted to IDictionary<string,object> or IDictionary<string,dynamic>
                // json arrays can be casted to IList<object> or IList<dynamic>

                // for this particular request we can guarantee that the result is 
                // always IDictionary<string,object>.
                var result = (IDictionary<string, object>)fb.Get("me");

                // make sure to cast the object to appropriate type
                var id = (string)result["id"];

                // FacebookClient's Get/Post/Delete methods only support JSON response results.
                // For non json results, you will need to use different mechanism,

                // here is an example for pictures.
                // available picture types: square (50x50), small (50xvariable height), large (about 200x variable height) (all size in pixels)
                // for more info visit http://developers.facebook.com/docs/reference/api
                string profilePictureUrl = string.Format("https://graph.facebook.com/{0}/picture?type={1}", id, "square");

                picProfile.LoadAsync(profilePictureUrl);

                //// another place where the response is not a json result is when
                //// exchanging code for access token.
                //var oauth = new FacebookOAuthClient
                //                {
                //                    AppId = "appId",
                //                    AppSecret = "apSecret",
                //                    RedirectUri = new System.Uri("http://redirecturi.com")
                //                };
                //var codeExchangeResult = (IDictionary<string, object>)oauth.ExchangeCodeForAccessToken("code");
                //var accessToken = codeExchangeResult["access_token"];
            }
            catch (FacebookApiException ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void GraphApiExample()
        {
            // note: avoid using synchronous methods if possible as it will block the thread until the result is receive

            try
            {
                var fb = new FacebookClient(_accessToken);

                // instead of casting to IDictionary<string,object> or IList<object>
                // you can also make use of the dynamic keyword.
                dynamic result = fb.Get("me");

                // You can either access it this way, using the .
                dynamic id = result.id;
                dynamic name = result.name;
                //dynamic username = result.username;

                //Microsoft.Win32.RegistryKey key;
                //key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("WowSocial", true);
                
                // if dynamic you don't need to cast explicitly.
                lblUserId.Text = "User Id: " + id;
                lnkName.Text = "Hi " + name;
                lnkName.LinkClicked += (o, e) => System.Diagnostics.Process.Start(result.link);

                // or using the indexer
                dynamic firstName = result["first_name"];
                dynamic lastName = result["last_name"];

                // checking if property exist
                var localeExists = result.ContainsKey("locale");

                // you can also cast it to IDictionary<string,object> and then check
                var dictionary = (IDictionary<string, object>)result;
                localeExists = dictionary.ContainsKey("locale");
            }
            catch (FacebookApiException ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void GraphApiAsyncExample()
        {
            var fb = new FacebookClient(_accessToken);

            // make sure to add the appropriate event handler
            // before calling the async methods.
            // GetCompleted     => GetAsync
            // PostCompleted    => PostAsync
            // DeleteCompleted  => DeleteAsync
            fb.GetCompleted += (o, e) =>
            {
                // incase you support cancellation, make sure to check
                // e.Cancelled property first even before checking (e.Error!=null).
                if (e.Cancelled)
                {
                    // for this example, we can ignore as we don't allow this
                    // example to be cancelled.

                    // you can check e.Error for reasons behind the cancellation.
                    var cancellationError = e.Error;
                }
                else if (e.Error != null)
                {
                    // error occurred
                    this.BeginInvoke(new MethodInvoker(
                                                 () =>
                                                 {
                                                     //MessageBox.Show(e.Error.Message);
                                                 }));
                }
                else
                {
                    // the request was completed successfully

                    // now we can either cast it to IDictionary<string, object> or IList<object>
                    // depending on the type.
                    // For this example, we know that it is IDictionary<string,object>.
                    var result = (IDictionary<string, object>)e.GetResultData();

                    var firstName = (string)result["first_name"];
                    var lastName = (string)result["last_name"];

                    // since this is an async callback, make sure to be on the right thread
                    // when working with the UI.
                    this.BeginInvoke(new MethodInvoker(
                                         () =>
                                         {
                                             lblFirstName.Text = "First Name: " + firstName;
                                         }));
                }
            };

            // additional parameters can be passed and 
            // must be assignable from IDictionary<string, object>
            var parameters = new Dictionary<string, object>();
            parameters["fields"] = "first_name,last_name";

            fb.GetAsync("me", parameters);
        }

        private void GraphApiAsyncDynamicExample()
        {
            var fb = new FacebookClient(_accessToken);

            // make sure to add the appropriate event handler
            // before calling the async methods.
            // GetCompleted     => GetAsync
            // PostCompleted    => PostAsync
            // DeleteCompleted  => DeleteAsync
            fb.GetCompleted += (o, e) =>
            {
                // incase you support cancellation, make sure to check
                // e.Cancelled property first even before checking (e.Error!=null).
                if (e.Cancelled)
                {
                    // for this example, we can ignore as we don't allow this
                    // example to be cancelled.

                    // you can check e.Error for reasons behind the cancellation.
                    var cancellationError = e.Error;
                }
                else if (e.Error != null)
                {
                    // error occurred
                    this.BeginInvoke(new MethodInvoker(
                                                 () =>
                                                 {
                                                     //MessageBox.Show(e.Error.Message);
                                                 }));
                }
                else
                {
                    // the request was completed successfully

                    // now we can either cast it to IDictionary<string, object> or IList<object>
                    // depending on the type. or we could use dynamic.
                    dynamic result = e.GetResultData();

                    // you can use either the . 
                    var firstName = result.first_name;

                    // or you can use indexer.
                    var lastName = result["last_name"];

                    // since this is an async callback, make sure to be on the right thread
                    // when working with the UI.
                    this.BeginInvoke(new MethodInvoker(
                                         () =>
                                         {
                                             lblLastName.Text = "Last Name: " + lastName;
                                         }));
                }
            };

            // additional parameters can be passed and 
            // must be assignable from IDictionary<string, object>
            // You can use ExpandoObject if you want to use dynamic
            dynamic parameters = new ExpandoObject();
            parameters.fields = "first_name,last_name";

            fb.GetAsync("me", parameters);
        }

        private void GraphApiParametersInPathExample()
        {
            // rather then creating a new object for parameter
            // you can also embed simple parameters as part of the path.

            try
            {
                var fb = new FacebookClient(_accessToken);

                dynamic result = fb.Get("me?fields=first_name,last_name");

                dynamic firstName = result.first_name;
                dynamic lastName = result.last_name;

                //// this is especially useful for paged data (result.paging.next and result.paging.previous)
                //// and your path can also contain the full graph url (https://graph.facebook.com/"
                //var nextPath = "https://graph.facebook.com/me/likes?limit=3&access_token=xxxxxxxxxxx&offset=3";
                //dynamic nextResult = fb.Get(nextPath);
            }
            catch (FacebookApiException ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void LegacyRestApiAsyncExample()
        {

            var fb = new FacebookClient(_accessToken);

            // make sure to add the appropriate event handler
            // before calling the async methods.
            // GetCompleted     => GetAsync
            // PostCompleted    => PostAsync
            // DeleteCompleted  => DeleteAsync
            fb.GetCompleted += (o, e) =>
            {
                // incase you support cancellation, make sure to check
                // e.Cancelled property first even before checking (e.Error!=null).
                if (e.Cancelled)
                {
                    // for this example, we can ignore as we don't allow this
                    // example to be cancelled.

                    // you can check e.Error for reasons behind the cancellation.
                    var cancellationError = e.Error;
                }
                else if (e.Error != null)
                {
                    // error occurred
                    this.BeginInvoke(new MethodInvoker(
                                                 () =>
                                                 {
                                                     //MessageBox.Show(e.Error.Message);
                                                 }));
                }
                else
                {
                    // the request was completed successfully

                    // now we can either cast it to IDictionary<string, object> or IList<object>
                    // depending on the type. or we could use dynamic.
                    dynamic result = e.GetResultData();

                    // or you could also use the generic version;
                    bool isFan = e.GetResultData<bool>();

                    // since this is an async callback, make sure to be on the right thread
                    // when working with the UI.
                    this.BeginInvoke(new MethodInvoker(
                                         () =>
                                         {
                                             chkCSharpSdkFan.Checked = isFan;
                                         }));
                }
            };

            dynamic parameters = new ExpandoObject();
            // any parameter that has "method" automatically is treated as rest api.
            parameters.method = "pages.isFan";
            parameters.page_id = "471653766213114";  // id of http://www.facebook.com/csharpsdk official page

            // for rest api only, parameters is enough
            // the rest method is determined by parameters.method
            fb.GetAsync(parameters);
        }

        private void FqlAsyncExample()
        {
            var fb = new FacebookClient(_accessToken);

            // since FQL is internally a GET request,
            // make sure to add the GET event handler.
            fb.GetCompleted += (o, e) =>
            {
                // incase you support cancellation, make sure to check
                // e.Cancelled property first even before checking (e.Error!=null).
                if (e.Cancelled)
                {
                    // for this example, we can ignore as we don't allow this
                    // example to be cancelled.

                    // you can check e.Error for reasons behind the cancellation.
                    var cancellationError = e.Error;
                }
                else if (e.Error != null)
                {
                    // error occurred
                    this.BeginInvoke(new MethodInvoker(
                                                 () =>
                                                 {
                                                     //MessageBox.Show(e.Error.Message);
                                                 }));
                }
                else
                {
                    // the request was completed successfully

                    // now we can either cast it to IDictionary<string, object> or IList<object>
                    // depending on the type. or we could use dynamic.
                    var result = (IList<object>)e.GetResultData();

                    var count = result.Count;

                    // since this is an async callback, make sure to be on the right thread
                    // when working with the UI.
                    this.BeginInvoke(new MethodInvoker(
                                         () =>
                                         {
                                             lblTotalFriends.Text = string.Format("You have {0} friend(s).", count);
                                         }));
                }
            };

            // query to get all the friends
            var query = string.Format("SELECT uid,pic_square FROM user WHERE uid IN (SELECT uid2 FROM friend WHERE uid1={0})", "me()");

            // call the Query or QueryAsync method to execute a single fql query.
            fb.QueryAsync(query);
        }

        private void FqlMultiQueryAsyncExample()
        {
            var fb = new FacebookClient(_accessToken);

            // since FQL multi-query is internally a GET request,
            // make sure to add the GET event handler.
            fb.GetCompleted += (o, e) =>
            {
                // incase you support cancellation, make sure to check
                // e.Cancelled property first even before checking (e.Error!=null).
                if (e.Cancelled)
                {
                    // for this example, we can ignore as we don't allow this
                    // example to be cancelled.

                    // you can check e.Error for reasons behind the cancellation.
                    var cancellationError = e.Error;
                }
                else if (e.Error != null)
                {
                    // error occurred
                    this.BeginInvoke(new MethodInvoker(
                                                 () =>
                                                 {
                                                     //MessageBox.Show(e.Error.Message);
                                                 }));
                }
                else
                {
                    // the request was completed successfully

                    // now we can either cast it to IDictionary<string, object> or IList<object>
                    // depending on the type. or we could use dynamic.
                    dynamic result = e.GetResultData();

                    dynamic resultForQuery1 = result[0].fql_result_set;
                    dynamic resultForQuery2 = result[1].fql_result_set;

                    var uid = resultForQuery1[0].uid;

                    this.BeginInvoke(new MethodInvoker(
                                         () =>
                                         {
                                             // make sure to be on the right thread when working with ui.
                                         }));
                }
            };

            var query1 = "SELECT uid FROM user WHERE uid=me()";
            var query2 = "SELECT profile_url FROM user WHERE uid=me()";

            // call the Query or QueryAsync method to execute a single fql query.
            // if there is more than one query Query/QueryAsync method will automatically
            // treat it as multi-query.
            fb.QueryAsync(new[] { query1, query2 });
        }

        private void BatchRequestExample()
        {
            try
            {
                var fb = new FacebookClient(_accessToken);
                dynamic result = fb.Batch(
                    new FacebookBatchParameter { HttpMethod = HttpMethod.Get, Path = "/4" },
                    new FacebookBatchParameter(HttpMethod.Get, "/me/friend", new Dictionary<string, object> { { "limit", 10 } }), // this should throw error
                    new FacebookBatchParameter("/me/friends", new { limit = 1 }) { Data = new { name = "one-friend", omit_response_on_success = false } }, // use Data to add additional parameters that doesn't exist
                    new FacebookBatchParameter { Parameters = new { ids = "{result=one-friend:$.data.0.id}" } },
                    new FacebookBatchParameter("{result=one-friend:$.data.0.id}/feed", new { limit = 5 }),
                    new FacebookBatchParameter().Query("SELECT name FROM user WHERE uid="), // fql
                    new FacebookBatchParameter().Query("SELECT first_name FROM user WHERE uid=me()", "SELECT last_name FROM user WHERE uid=me()") // fql multi-query
                    //,new FacebookBatchParameter(HttpMethod.Post, "/me/feed", new { message = "test status update" })
                    );

                // always remember to check individual errors for the batch requests.
                if (result[0] is Exception)
                    MessageBox.Show(((Exception)result[0]).Message);
                dynamic first = result[0];
                string name = first.name;

                // note: incase the omit_response_on_success = true, result[x] == null

                // for this example, just comment it out.
                //if (result[1] is Exception)
                //    MessageBox.Show(((Exception)result[1]).Message);
                //if (result[2] is Exception)
                //    MessageBox.Show(((Exception)result[1]).Message);
                //if (result[3] is Exception)
                //    MessageBox.Show(((Exception)result[1]).Message);
                //if (result[4] is Exception)
                //    MessageBox.Show(((Exception)result[1]).Message);
                //if (result[5] is Exception)
                //    MessageBox.Show(((Exception)result[1]).Message);
                //if (result[6] is Exception)
                //    MessageBox.Show(((Exception)result[1]).Message);
                //if (result[7] is Exception)
                //    MessageBox.Show(((Exception)result[1]).Message);
            }
            catch (FacebookApiException ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void BatchRequestAsyncExample()
        {
            var fb = new FacebookClient(_accessToken);

            // since batch request is actually a POST request internally,
            // make sure to add the event handler for PostCompleted.
            fb.PostCompleted += (o, e) =>
            {
                // incase you support cancellation, make sure to check
                // e.Cancelled property first even before checking (e.Error!=null).
                if (e.Cancelled)
                {
                    // for this example, we can ignore as we don't allow this
                    // example to be cancelled.

                    // you can check e.Error for reasons behind the cancellation.
                    var cancellationError = e.Error;
                }
                else if (e.Error != null)
                {
                    // error occurred
                    this.BeginInvoke(new MethodInvoker(
                                                 () =>
                                                 {
                                                     //MessageBox.Show(e.Error.Message);
                                                 }));
                }
                else
                {
                    // the request was completed successfully

                    // now we can either cast it to IDictionary<string, object> or IList<object>
                    // depending on the type. or we could use dynamic.
                    dynamic result = e.GetResultData();

                    // note: batch requests doesn't support generic versions of e.GetResultData<T>()

                    // make sure to be on the right thread when working with ui.
                    this.BeginInvoke(new MethodInvoker(
                                         () =>
                                         {
                                             // always remember to check individual errors for the batch requests.
                                             if (result[0] is Exception)
                                                 MessageBox.Show(((Exception)result[0]).Message);
                                             dynamic first = result[0];
                                             string name = first.name;

                                             // note: incase the omit_response_on_success = true, result[x] == null

                                             // for this example just comment it out
                                             //if (result[1] is Exception)
                                             //    MessageBox.Show(((Exception)result[1]).Message);
                                             //if (result[2] is Exception)
                                             //    MessageBox.Show(((Exception)result[1]).Message);
                                             //if (result[3] is Exception)
                                             //    MessageBox.Show(((Exception)result[1]).Message);
                                             //if (result[4] is Exception)
                                             //    MessageBox.Show(((Exception)result[1]).Message);
                                             //if (result[5] is Exception)
                                             //    MessageBox.Show(((Exception)result[1]).Message);
                                             //if (result[6] is Exception)
                                             //    MessageBox.Show(((Exception)result[1]).Message);
                                             //if (result[7] is Exception)
                                             //    MessageBox.Show(((Exception)result[1]).Message);
                                         }));
                }
            };

            fb.BatchAsync(new[]{
                new FacebookBatchParameter { HttpMethod = HttpMethod.Get, Path = "/4" },
                new FacebookBatchParameter(HttpMethod.Get, "/me/friend", new Dictionary<string, object> { { "limit", 10 } }), // this should throw error
                new FacebookBatchParameter("/me/friends", new { limit = 1 }) { Data = new { name = "one-friend", omit_response_on_success = false } }, // use Data to add additional parameters that doesn't exist
                new FacebookBatchParameter { Parameters = new { ids = "{result=one-friend:$.data.0.id}" } },
                new FacebookBatchParameter("{result=one-friend:$.data.0.id}/feed", new { limit = 5 }),
                new FacebookBatchParameter().Query("SELECT name FROM user WHERE uid="), // fql
                new FacebookBatchParameter().Query("SELECT first_name FROM user WHERE uid=me()", "SELECT last_name FROM user WHERE uid=me()") // fql multi-query
                //,new FacebookBatchParameter(HttpMethod.Post, "/me/feed", new { message = "test status update" })
            });
        }

        private string _lastMessageId;

        private void btnPostToWall_Click(object sender, EventArgs args)
        {
            
            //// Scrape links from wikipedia.org
            //string quest = "On+to+Kharanos";
           
            //// 1.
            //// URL: http://en.wikipedia.org/wiki/Main_Page
            //WebClient w = new WebClient();
            //string s = w.DownloadString("http://es.wowhead.com/search?q=" + quest + "#quests");

            //// 2.
            //foreach (LinkItem i in LinkFinder.Find(s))
            //{
            //    Debug.WriteLine(i);
            //    //Link = i;
            //    if (i.Text.Contains(quest.Replace("+","_")))
            //    {
            //        link = i.Text;
            //    }

            //}

            var fb = new FacebookClient(_accessToken);

            // make sure to add event handler for PostCompleted.
            fb.PostCompleted += (o, e) =>
            {
                // incase you support cancellation, make sure to check
                // e.Cancelled property first even before checking (e.Error!=null).
                if (e.Cancelled)
                {
                    // for this example, we can ignore as we don't allow this
                    // example to be cancelled.

                    // you can check e.Error for reasons behind the cancellation.
                    var cancellationError = e.Error;
                }
                else if (e.Error != null)
                {
                    // error occurred
                    this.BeginInvoke(new MethodInvoker(
                                                 () =>
                                                 {
                                                     //MessageBox.Show(e.Error.Message);
                                                 }));
                }
                else
                {
                    // the request was completed successfully

                    // now we can either cast it to IDictionary<string, object> or IList<object>
                    // depending on the type. or we could use dynamic.
                    dynamic result = e.GetResultData();
                    _lastMessageId = result.id;

                    // make sure to be on the right thread when working with ui.
                    this.BeginInvoke(new MethodInvoker(
                                         () =>
                                         {
                                             MessageBox.Show("Message Posted successfully");

                                             //txtMessage.Text = string.Empty;
                                             //btnDeleteLastMessage.Enabled = true;
                                         }));
                }
            };

            dynamic parameters = new ExpandoObject();
            parameters.privacy = new {
	        value = "ALL_FRIENDS",
            };
            //parameters.message = txtMessage.Text;
            parameters.name = "Mision Aceptada";
            parameters.link = "http://www.google.com";
            parameters.caption = "Una prueba de atachment";
            fb.PostAsync("/me/feed", parameters);

        }

        private void Publicar(string arg1, string name, string time, string message = "")
        {
            try
            {
                string test = GetPageAccessToken(_accessToken);
                var Variab = new Variables();
                var fb = new FacebookClient(test);
            
            // make sure to add event handler for PostCompleted.
            fb.PostCompleted += (o, e) =>
            {
                // incase you support cancellation, make sure to check
                // e.Cancelled property first even before checking (e.Error!=null).
                if (e.Cancelled)
                {
                    // for this example, we can ignore as we don't allow this
                    // example to be cancelled.

                    // you can check e.Error for reasons behind the cancellation.
                    var cancellationError = e.Error;
                }
                else if (e.Error != null)
                {
                    // error occurred
                    this.BeginInvoke(new MethodInvoker(
                                                 () =>
                                                 {
                                                     //MessageBox.Show(e.Error.Message);
                                                 }));
                }
                else
                {
                    // the request was completed successfully

                    // now we can either cast it to IDictionary<string, object> or IList<object>
                    // depending on the type. or we could use dynamic.
                    dynamic result = e.GetResultData();
                    _lastMessageId = result.id;

                    // make sure to be on the right thread when working with ui.
                    this.BeginInvoke(new MethodInvoker(
                                         () =>
                                         {
                                             //MessageBox.Show("Message Posted successfully");

                                             //txtMessage.Text = string.Empty;
                                             //btnDeleteLastMessage.Enabled = true;
                                         }));
                }
            };

            dynamic parameters = new ExpandoObject();
            parameters.privacy = new
            {
                value = "ALL_FRIENDS",
            };
            parameters.name = name;
            parameters.message = message;
            if (arg1.Contains("/wow/en/item/") == true)
            {
                parameters.link = "http://us.battle.net" + arg1;
            }
            else
            {
                parameters.link = "http://us.battle.net/" + arg1;
            }

            //parameters.link = "http://us.battle.net/wow/en/character/quelthalas/Amadoflimd/" + arg1;
            //s.ResponseUri.AbsoluteUri
            parameters.caption = "Realm: " + Variables.Realm;
            fb.PostAsync("/" + Variables.PageID + "/feed", parameters);
            StreamWriter sw = new StreamWriter("C:\\UpdatesGuild.txt", true);
                
            //Write a line of text
            sw.WriteLine(name);
            //sw.WriteLine(Environment.NewLine);

            //sw.Flush();
            //Close the file
            sw.Close();
            }
            catch (Exception ex)
            {

            }
        }

        private void RefreshTokenAndPostToFacebook(string currentAccessToken)
        {
            string newAccessToken = RefreshAccessToken(currentAccessToken);
            pageAccessToken = GetPageAccessToken(newAccessToken);
            //PostToFacebook(pageAccessToken);
            //return newAccessToken; // replace current access token with this
        }

        public static string GetPageAccessToken(string userAccessToken)
        {
            FacebookClient fbClient = new FacebookClient();
            //fbClient.AppId = "app id";
            //fbClient.AppSecret = "app secret";
            fbClient.AccessToken = userAccessToken;
            Dictionary<string, object> fbParams = new Dictionary<string, object>();
            JsonObject publishedResponse = fbClient.Get("/me/accounts", fbParams) as JsonObject;
            JArray data = JArray.Parse(publishedResponse["data"].ToString());

            foreach (var account in data)
            {
              
                    return account["access_token"].ToString();
             
            }

            return String.Empty;
        }

        public static string RefreshAccessToken(string currentAccessToken)
        {
            FacebookClient fbClient = new FacebookClient();
            Dictionary<string, object> fbParams = new Dictionary<string, object>();
            fbParams["client_id"] = "app id";
            fbParams["grant_type"] = "fb_exchange_token";
            fbParams["client_secret"] = "app secret";
            fbParams["fb_exchange_token"] = currentAccessToken;
            JsonObject publishedResponse = fbClient.Get("/oauth/access_token", fbParams) as JsonObject;
            return publishedResponse["access_token"].ToString();
        }

        private void btnDeleteLastMessage_Click(object sender, EventArgs args)
        {
            //btnDeleteLastMessage.Enabled = false;

            var fb = new FacebookClient(_accessToken);

            // make sure to add event handler for DeleteCompleted.
            fb.DeleteCompleted += (o, e) =>
            {
                // incase you support cancellation, make sure to check
                // e.Cancelled property first even before checking (e.Error!=null).
                if (e.Cancelled)
                {
                    // for this example, we can ignore as we don't allow this
                    // example to be cancelled.

                    // you can check e.Error for reasons behind the cancellation.
                    var cancellationError = e.Error;
                }
                else if (e.Error != null)
                {
                    // error occurred
                    this.BeginInvoke(new MethodInvoker(
                                                 () =>
                                                 {
                                                     //MessageBox.Show(e.Error.Message);
                                                 }));
                }
                else
                {
                    // the request was completed successfully

                    // make sure to be on the right thread when working with ui.
                    this.BeginInvoke(new MethodInvoker(
                                         () =>
                                         {
                                             //MessageBox.Show("Message deleted successfully");
                                             //btnDeleteLastMessage.Enabled = false;
                                         }));
                }
            };

            fb.DeleteAsync(_lastMessageId);
        }

        private void bntPostPicture_Click(object sender, EventArgs args)
        {
            var ofd = new OpenFileDialog
                          {
                              Filter = "JPEG Files|*.jpg",
                              Title = "Select picture to upload"
                          };
            if (ofd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            var fb = new FacebookClient(_accessToken);

            // make sure to add event handler for PostCompleted.
            fb.PostCompleted += (o, e) =>
            {
                // incase you support cancellation, make sure to check
                // e.Cancelled property first even before checking (e.Error!=null).
                if (e.Cancelled)
                {
                    // for this example, we can ignore as we don't allow this
                    // example to be cancelled.

                    // you can check e.Error for reasons behind the cancellation.
                    var cancellationError = e.Error;
                }
                else if (e.Error != null)
                {
                    // error occurred
                    this.BeginInvoke(new MethodInvoker(
                                                 () =>
                                                 {
                                                     //MessageBox.Show(e.Error.Message);
                                                 }));
                }
                else
                {
                    // the request was completed successfully

                    // make sure to be on the right thread when working with ui.
                    this.BeginInvoke(new MethodInvoker(
                                         () =>
                                         {
                                             MessageBox.Show("Picture uploaded successfully");
                                         }));
                }
            };

            dynamic parameters = new ExpandoObject();
            //parameters.message = txtMessage.Text;
            parameters.link = "http://www.google.com";
            parameters.caption = "Una prueba de atachment";
            parameters.source = new FacebookMediaObject
                                    {
                                        ContentType = "image/jpeg",
                                        FileName = Path.GetFileName(ofd.FileName)
                                    }.SetValue(File.ReadAllBytes(ofd.FileName));

            fb.PostAsync("me/photos", parameters);
        }

        private void btnPostVideo_Click(object sender, EventArgs args)
        {
            var ofd = new OpenFileDialog
            {
                Filter = "MP4 Files|*.mp4",
                Title = "Select video to upload"
            };
            if (ofd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            var fb = new FacebookClient(_accessToken);

            // make sure to add event handler for PostCompleted.
            fb.PostCompleted += (o, e) =>
            {
                // incase you support cancellation, make sure to check
                // e.Cancelled property first even before checking (e.Error!=null).
                if (e.Cancelled)
                {
                    // for this example, we can ignore as we don't allow this
                    // example to be cancelled.

                    // you can check e.Error for reasons behind the cancellation.
                    var cancellationError = e.Error;
                }
                else if (e.Error != null)
                {
                    // error occurred
                    this.BeginInvoke(new MethodInvoker(
                                                 () =>
                                                 {
                                                     //MessageBox.Show(e.Error.Message);
                                                 }));
                }
                else
                {
                    // the request was completed successfully

                    // make sure to be on the right thread when working with ui.
                    this.BeginInvoke(new MethodInvoker(
                                         () =>
                                         {
                                             MessageBox.Show("Video uploaded successfully");
                                         }));
                }
            };

            dynamic parameters = new ExpandoObject();
            //parameters.message = txtMessage.Text;
            parameters.source = new FacebookMediaObject
            {
                ContentType = "video/mp4",
                FileName = Path.GetFileName(ofd.FileName)
            }.SetValue(File.ReadAllBytes(ofd.FileName));

            fb.PostAsync("me/videos", parameters);
        }

        private void lnkFacebokSdkFan_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://apps.facebook.com/wow_social_activity/");
        }

        private void btnProgressAndCancellation_Click(object sender, EventArgs e)
        {
            var dlg = new UploadProgressCancelForm(_accessToken);
            dlg.ShowDialog();
        }

        // This method accepts two strings the represent two files to 
        // compare. A return value of 0 indicates that the contents of the files
        // are the same. A return value of any other value indicates that the 
        // files are not the same.
        private bool FileCompare(string file1, string file2)
        {
            int file1byte;
            int file2byte;
            FileStream fs1;
            FileStream fs2;

            // Determine if the same file was referenced two times.
            if (file1 == file2)
            {
                // Return true to indicate that the files are the same.
                return true;
            }

            // Open the two files.
            fs1 = new FileStream(file1, FileMode.Open);
            fs2 = new FileStream(file2, FileMode.Open);

            // Check the file sizes. If they are not the same, the files 
            // are not the same.
            if (fs1.Length != fs2.Length)
            {
                // Close the file
                fs1.Close();
                fs2.Close();

                // Return false to indicate files are different
                return false;
            }

            // Read and compare a byte from each file until either a
            // non-matching set of bytes is found or until the end of
            // file1 is reached.
            do
            {
                // Read one byte from each file.
                file1byte = fs1.ReadByte();
                file2byte = fs2.ReadByte();
            }
            while ((file1byte == file2byte) && (file1byte != -1));

            // Close the files.
            fs1.Close();
            fs2.Close();

            // Return the success of the comparison. "file1byte" is 
            // equal to "file2byte" at this point only if the files are 
            // the same.
            return ((file1byte - file2byte) == 0);
        }
 
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            try
            {
             var Variab = new Variables();

             string PathLog = "http://us.battle.net/wow/en/guild/" + Variables.Realm + "/" + Variables.Guild + "/";
      

                try
                {

                    WebRequest request = WebRequest.Create(PathLog);
                    request.Timeout = 4000;
                    WebResponse response = request.GetResponse();
                    data = response.GetResponseStream();
                    response = null;
                    string html = String.Empty;
                   
                }
                catch (Exception ex)
                {

                }


            System.IO.StreamReader sr = new StreamReader(data);


            string line = null;
        
            while ((sr.Peek() >= 0))
            {
                string resultlist = null;
                line = sr.ReadLine();

                if (((line.Contains("/achievement#") == true) || (line.Contains("<a href=\"/wow/en/item/") == true)) && line.Contains("</a>") == true && line.Contains("class=\"icon\"") == false)
                {

                    string linea = line;
                    string url;

                    //linea.Replace("</a>", "");
                    ////linea.Replace('\t', string.Empty);
                    //int index1 = linea.IndexOf('\t');                        // 2B.
                    //int index2 = linea.IndexOf('t', index1 + 1);

                    int index1;
                    int index2;

                    if (line.Contains("The guild") == true)
                   {
                       linea = linea.Replace("&#39;", "");
                       index1 = linea.IndexOf('"');                        // 2B.
                       index2 = linea.IndexOf('"', index1 + 1);
                       url = linea.Substring(index1 + 1, (index2) - (index1 + 1));
                       linea = linea.Remove(0, 1); // 2D.

                       index1 = linea.IndexOf('<');
                       index2 = linea.IndexOf('>', index1 + 1);
                       linea = linea.Remove(index1, (index2 + 1) - index1); // 2D.

                       index1 = linea.IndexOf('<');
                       index2 = linea.IndexOf('>', index1 + 1);
                       linea = linea.Remove(index1, (index2 + 1) - index1); // 2D.
                   }
                   else
                   {
                    linea = linea.Replace("&#39;", "");
                    index1 = linea.IndexOf('<');
                    index2 = linea.IndexOf('>', index1 + 1);
                    linea = linea.Remove(index1, (index2 + 1) - index1); // 2D.

                    index1 = linea.IndexOf('"');                        // 2B.
                    index2 = linea.IndexOf('"', index1 + 1);
                    url = linea.Substring(index1 + 1, (index2) - (index1 + 1));
                    linea = linea.Remove(0, 1); // 2D.

                    index1 = linea.IndexOf('<');
                    index2 = linea.IndexOf('>', index1 + 1);
                    linea = linea.Remove(index1, (index2 + 1) - index1); // 2D.

                    index1 = linea.IndexOf('<');
                    index2 = linea.IndexOf('>', index1 + 1);
                    linea = linea.Remove(index1, (index2 + 1) - index1); // 2D.

                    index1 = linea.IndexOf('<');
                    index2 = linea.IndexOf('>', index1 + 1);
                    linea = linea.Remove(index1, (index2 + 1) - index1); // 2D.
                   }
                    if (line.Contains("Completed step") == true)
                    {
                    
                        //COMPLETED STEP
                        index1 = linea.IndexOf('<');
                        index2 = linea.IndexOf('>', index1 + 1);
                        linea = linea.Remove(index1, (index2 + 1) - index1); // 2D.

                        index1 = linea.IndexOf('<');
                        index2 = linea.IndexOf('>', index1 + 1);
                        linea = linea.Remove(index1, (index2 + 1) - index1); // 2D.

                        }
                    //linea = linea.Replace(">", "");

                    var items = new List<string>();

                    //File.Create("C:\\Updates.txt");
                    using (var stream = File.OpenRead("C:\\UpdatesGuild.txt"))  // open file
                    using (var reader = new StreamReader(stream))   // read the stream with TextReader
                    {
                        string linex;

                        // read until no more lines are present
                        while ((linex = reader.ReadLine()) != null)
                        {
                            items.Add(linex);
                        }
                        reader.Close();
                    }
                    

                    foreach (string result in items) // Loop through List with foreach
                    {
                        if (result == linea)
                        {
                            resultlist = linea;
                        }
                    }
                    if (resultlist != linea)
                    {
                            double times;
                            times = Convert.ToDouble(time);
                            list.Add(linea);
                            //arg1 = arg1.Replace("Quest accepted: ", "");
                            Tiempo(1);
                            DateTime NextMonth = DateAndTime.DateAdd("s", times, "01/01/1970 00:00:00");
                            Publicar(url, linea, NextMonth.ToString(), linea);
                            
                }


                    


                }
                //if (line.Contains("[\"type\"]") == true)
                //{
                //    type = line.Replace("[\"type\"] ", "");
                //    type = type.Replace("= ", "");
                //    type = type.Replace(",", "");
                //    type = type.Replace("\t", "");
                //    type = type.Replace(@"""", "");
                //    //type = line.Replace(" ", "");


                //}

                //if (line.Contains("[\"time\"]") == true)
                //{
                //    time = line.Replace("[\"time\"] ", "");
                //    time = time.Replace("= ", "");
                //    time = time.Replace(",", "");
                //    time = time.Replace("\t", "");
                //    time = time.Replace(@"""", "");
                //    //type = line.Replace(" ", "");


                //}

                //if (line.Contains("[\"arg1\"]") == true)
                //{
                //    arg1 = line.Replace("[\"arg1\"] ", "");
                //    arg1 = arg1.Replace("= ", "");
                //    arg1 = arg1.Replace(",", "");
                //    arg1 = arg1.Replace("\t", "");
                //    arg1 = arg1.Replace(@"""", "");
                //    //arg1 = line.Replace(" ", "");


                //}

                //if (line.Contains("[\"arg2\"]") == true)
                //{
                //    arg2 = line.Replace("[\"arg2\"] ", "");
                //    arg2 = arg2.Replace("= ", "");
                //    arg2 = arg2.Replace(",", "");
                //    arg2 = arg2.Replace("\t", "");
                //    arg2 = arg2.Replace(@"""", "");
                //    //arg2 = line.Replace(" ", "");

                //}
            }

                sr.Dispose();
                //sr2.Dispose();
                //File.Delete("C:/ElephantFB.lua");
                //File.Copy("C:/Elephant.lua", "C:/ElephantFB.lua");
            //while 
            //{
            //}
        }
               catch (Exception ex)

            {
                //MessageBox.Show(ex.Message);
            }
            timer1.Start();
    }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Application was develop by Agustin Rudas","ABOUT");
            System.Diagnostics.Process.Start("http://www.facebook.com/AGUSTIN.RUDAS");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Donate Options: \n Panama: \n Banco General: \n Cuenta: 04-01-01-609729-1 \n Nombre: Agustin Rudas \n\n Worldwide: \n Paypal: agustinrudas@live.com", "DONATE");
        }



 }
         

}

      