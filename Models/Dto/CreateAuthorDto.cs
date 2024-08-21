namespace BookStore.Dto
{
	public class CreateAuthorDTO
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Bio { get; set; }
		public DateTime? DateOfBirth { get; set; }
        public object?[]? Id { get; internal set; }
    }
}