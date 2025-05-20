using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDC.Backend.DataRepository.Test;
using TDC.Backend.DataRepository;

namespace TDC.Backend.Test.RepositoryTests
{
    public class CharacterRepositoryTests
    {
        private CharacterRepository _target;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var connectionFactory = TestStartUp.GetConnectionFactory();
            this._target = new CharacterRepository(connectionFactory);
        }

        [SetUp]
        public void Setup()
        {
            this._target.CleanTable();
        }

        [TearDown]
        public void TearDown()
        {
            this._target.CleanTable();
        }
    }
}
