using Newtonsoft.Json;
using RestSharp;
using System.Text.Json.Nodes;

namespace RestSharpDemo
{
    public class Employee
    {
        public int id { get; set; }
        public string name { get; set; }
        public int salary { get; set; }
    }
    [TestClass]
    public class RestSharpTestCase
    {
        RestClient client;

        [TestInitialize]
        public void Setup()
        {
            client = new RestClient("http://localhost:4000");

        }

        private IRestResponse getEmployeeList()
        {
            RestRequest request = new RestRequest("/employees", Method.GET);

            IRestResponse response = client.Execute(request);
            return response;
        }

        [TestMethod]
        public void onCallingGetAi_ReturnEmployeeList()
        {
            IRestResponse response = getEmployeeList();

            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            List<Employee> dataresponse = JsonConvert.DeserializeObject<List<Employee>>(response.Content);

            Assert.AreEqual(4, dataresponse.Count);
        }

        [TestMethod]
        public void givenEmployee_OnPost_ShouldReturnAddEmployee()
        {
            RestRequest request = new RestRequest("/employees", Method.POST);
            System.Text.Json.Nodes.JsonObject jsonObject = new System.Text.Json.Nodes.JsonObject();

            jsonObject.Add("name", "Clark");
            jsonObject.Add("salary", 17000);

            request.AddParameter("application/json", jsonObject, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.Created);

            Employee dataresponse = JsonConvert.DeserializeObject<Employee>(response.Content);

            Assert.AreEqual("Clark", dataresponse.name);
            Assert.AreEqual(17000, dataresponse.salary);

            Console.WriteLine(response.Content);
        }

        [TestMethod]
        public void deleteEmployee()
        {
            RestRequest request = new RestRequest("/employees/5", Method.DELETE);

            IRestResponse response = client.Execute(request);

            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            Console.WriteLine(response.Content);
        }

        [TestMethod]
        public void givenEmployee_OnPut_ShouldReturnAddEmployee()
        {
            RestRequest request = new RestRequest("/employees/1", Method.PUT);
            System.Text.Json.Nodes.JsonObject jsonObject = new System.Text.Json.Nodes.JsonObject();

            jsonObject.Add("name", "shiv");
            jsonObject.Add("salary", 19000);

            request.AddParameter("application/json", jsonObject, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);

            Employee dataresponse = JsonConvert.DeserializeObject<Employee>(response.Content);

            Assert.AreEqual("shiv", dataresponse.name);
            Assert.AreEqual(19000, dataresponse.salary);

            Console.WriteLine(response.Content);
        }
    }
}