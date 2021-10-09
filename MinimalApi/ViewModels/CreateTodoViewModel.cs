using Flunt.Notifications;
using Flunt.Validations;

namespace MinimalApi.ViewModels
{
    public class CreateTodoViewModel : Notifiable<Notification>
    {
        public string Title { get; set; }    

        public Todo ValidateAndMapTo(){
            var contract = new Contract<Notification>()
                .Requires()
                .IsNotNull(Title, "Title is could not be null")
                .IsGreaterThan(Title, 5, "Titlle must contai more than 5 characters");
            
            AddNotifications(contract);

            return new Todo(Guid.NewGuid(), Title, false);
        }
    }
}