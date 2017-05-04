using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolveMath.Controllers;
using SolveMath.Data.Interfaces;
using SolveMath.Data.Mocks;
using SolveMath.Models.Entities;
using SolveMath.Services.Contracts;
using TestStack.FluentMVCTesting;

namespace SolveMath.Tests.Controllers
{
    [TestClass]
    public class ManageControllerTest
    {
        private ManageController _controller;

        [TestInitialize]
        public void Init()
        {
            _controller = new ManageController();
        }
        [TestMethod]
        public void DeleteTopic()
        {
            //_controller.WithCallTo(manageController=>
            //manageController.DeleteTopic(id))
        }
    }
}