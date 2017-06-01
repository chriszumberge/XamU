using System;
using System.Linq;
using Xamarin.Forms;

namespace MyContacts
{
    public partial class ContactDetails : ContentPage
    {
        readonly Person person;

		public ContactDetails(Person person)
        {
            this.person = person;
            this.BindingContext = person;

            InitializeComponent();

            //nameEntry.Text = person.Name;
            //nameEntry.TextChanged += (sender, e) => person.Name = nameEntry.Text;

            //Binding nameBinding = new Binding();
            //nameBinding.Source = person;
            //nameBinding.Path = nameof(person.Name);
            //nameEntry.SetBinding(Entry.TextProperty, nameBinding);

            //nameEntry.SetBinding<Person>(Entry.TextProperty, data => data.Name, BindingMode.TwoWay);

            // Setup person
            //headshot.Source = ImageSource.FromResource("MyContacts.Images." + person.HeadshotUrl);
            //emailEntry.Text = person.Email;
            //birthday.Date = person.Dob;
            //favoriteSwitch.IsToggled = person.IsFavorite;
            //genderPicker.SelectedIndex = (person.Gender == Gender.Male) ? 0 : 1;

            // Handle changes
            //emailEntry.TextChanged += (sender, e) => person.Email = emailEntry.Text;
            //birthday.DateSelected += (sender, e) => person.Dob = birthday.Date;
            //favoriteSwitch.Toggled += (sender, e) => person.IsFavorite = favoriteSwitch.IsToggled;
            //genderPicker.SelectedIndexChanged += (sender, e) => person.Gender = genderPicker.SelectedIndex == 0 ? Gender.Male : Gender.Female;
        }

        void OnShow(object sender, EventArgs e)
        {
            person.Dob = person.Dob.AddYears(1);
            this.DisplayAlert("Selected Contact", person.ToString(), "OK");
        }
    }
}
