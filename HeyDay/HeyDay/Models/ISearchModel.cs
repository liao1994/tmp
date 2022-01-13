namespace HeyDay.Controllers
{
	public interface ISearchModel
	{
		string Query { get; set; }
		bool Images { get; }
	}
}