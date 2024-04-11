
using HelloWorld240318.Models;

namespace HelloWorld240318.Service
{
    public interface IUsersDetailOneService
    {
        public List<ViewModels.UsersDetailOne> GetAllData(QueryModel.UsersDetailOne qModel);

        public ViewModels.UsersDetailOne ChangeToViewModel(UsersDetailOne m);

        public ViewModels.UsersDetailOne EditPage(long? id);

        public void UpDateData(ViewModels.UsersDetailOne m);

        public void DeleteData(long? id);
    }
}
