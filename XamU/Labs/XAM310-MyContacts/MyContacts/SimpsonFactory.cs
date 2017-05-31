using System;

namespace MyContacts
{
	public static class SimpsonFactory
	{
		public static Person GetPerson()
		{
			return new Person { 
				Name = "Homer Simpson", 
				HeadshotUrl = "Homer.gif", 
				Email = "donutlover@aol.com", 
				Dob = new DateTime(1965, 1, 24),
				Gender = Gender.Male,
				IsFavorite = false,
			};
		}
    }
}

