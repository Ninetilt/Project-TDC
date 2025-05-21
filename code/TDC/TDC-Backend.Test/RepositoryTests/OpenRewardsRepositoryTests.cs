using TDC.Backend.DataRepository.Test;
using TDC.Backend.DataRepository;

namespace TDC.Backend.Test.RepositoryTests
{
    public class OpenRewardsRepositoryTests
    {
        private OpenRewardsRepository _target;
        private ListRepository _listRepository;
        private AccountRepository _accountRepository;
        private ListRewardingRepository _listRewardingRepository;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var connectionFactory = TestStartUp.GetConnectionFactory();
            this._target = new OpenRewardsRepository(connectionFactory);
            _listRepository = new ListRepository(connectionFactory);
            _accountRepository = new AccountRepository(connectionFactory);
            _listRewardingRepository = new ListRewardingRepository(connectionFactory);
        }

        [SetUp]
        public void Setup()
        {
            this._target.CleanTable();
            _listRepository.CleanTable();
            _accountRepository.CleanTable();
            _listRewardingRepository.CleanTable();
        }

        [TearDown]
        public void TearDown()
        {
            this._target.CleanTable();
            _listRepository.CleanTable();
            _accountRepository.CleanTable();
            _listRewardingRepository.CleanTable();
        }
    }
}
