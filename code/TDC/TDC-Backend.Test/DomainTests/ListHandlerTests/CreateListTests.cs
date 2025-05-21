using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using TDC.Backend.Domain;
using TDC.Backend.IDataRepository;
using TDC.Backend.IDataRepository.Models;
using TDC.Backend.IDomain.Models;

namespace TDC.Backend.Test.DomainTests.ListHandlerTests
{
    public class CreateListTests
    {
        private ToDoListHandler _target;
        private IListRepository _listRepository;
        private IListItemRepository _listItemRepository;
        private IListMemberRepository _listMemberRepository;
        private IListInvitationRepository _listInvitationRepository;
        private IListRewardingRepository _listRewardingRepository;
        private IOpenRewardsRepository _openRewardsRepository;

        [SetUp]
        public void SetUp()
        {
            _listRepository = Substitute.For<IListRepository>();
            _listItemRepository = Substitute.For<IListItemRepository>();
            _listMemberRepository = Substitute.For<IListMemberRepository>();
            _listInvitationRepository = Substitute.For<IListInvitationRepository>();
            _listRewardingRepository = Substitute.For<IListRewardingRepository>();
            _openRewardsRepository = Substitute.For<IOpenRewardsRepository>();
            _target = new ToDoListHandler(_listRepository, _listItemRepository, _listMemberRepository, _listInvitationRepository, _listRewardingRepository, _openRewardsRepository);

            _target._listRepository.CreateList(Arg.Any<ToDoListDbo>()).Returns(1);
        }

        [Test]
        public void CreateList_CallsListRepositoryAndAddsCreatorToMemberList()
        {
            var listDto = new ToDoListSavingDto("test-list", false);
            _target.CreateList("test-user", listDto);

            _target._listRepository.Received().CreateList(Arg.Is<ToDoListDbo>(dbo =>
                                                                                 dbo.Id == 0 &&
                                                                                 dbo.Name == listDto.Name &&
                                                                                 dbo.IsCollaborative == listDto.IsCollaborative &&
                                                                                 dbo.IsFinished == false
                                                                            ));
            _target._listMemberRepository.Received().AddListMember(1, "test-user", true);
        }
    }
}
