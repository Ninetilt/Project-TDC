﻿using NSubstitute;
using TDC.Backend.Domain;
using TDC.Backend.IDataRepository.Models;
using TDC.Backend.IDataRepository;

namespace TDC.Backend.Test.DomainTests.ListHandlerTests
{
    public class AddItemToListTests
    {
        private ToDoListHandler _target;
        private IListRepository _listRepository;
        private IListItemRepository _listItemRepository;
        private IListMemberRepository _listMemberRepository;

        [SetUp]
        public void SetUp()
        {
            _listRepository = Substitute.For<IListRepository>();
            _listItemRepository = Substitute.For<IListItemRepository>();
            _listMemberRepository = Substitute.For<IListMemberRepository>();
            _target = new ToDoListHandler(_listRepository, _listItemRepository, _listMemberRepository);
        }

        [Test]
        public void AddItemToList_ListDoesNotExist_DoesNotCallRepository()
        {
            _target.AddItemToList(1, "", 1);
            _target._listItemRepository.DidNotReceive().AddItemToList(Arg.Any<long>(), Arg.Any<ToDoListItemDbo>());

        }

        [Test]
        public void AddItemToList_ListExists_CallsRepository()
        {
            _target._listRepository.GetById(1).Returns(new ToDoListDbo(1, "", true, false));
            _target.AddItemToList(1, "test-item", 2);
            _target._listItemRepository.Received().AddItemToList(1, Arg.Is<ToDoListItemDbo>(dbo =>
                                                                                            dbo.ItemId == 0 &&
                                                                                            dbo.Description == "test-item" &&
                                                                                            dbo.Effort == 2 
                                                                                       ));
        }
    }
}
