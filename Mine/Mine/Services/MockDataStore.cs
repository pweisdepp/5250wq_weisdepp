using Mine.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mine.Services
{
    public class MockDataStore : IDataStore<ItemModel>
    {
        /// <summary>
        /// The Data List
        /// This is where the items are stored
        /// </summary>
        public List<ItemModel> datalist;

        /// <summary>
        /// Constructor for the Storee
        /// </summary>
        public MockDataStore()
        {
            // Load the dataset
            LoadDefaultData();
        }

        /// <summary>
        /// Load the Default data
        /// </summary>
        /// <returns></returns>
        public bool LoadDefaultData()
        {
            datalist = new List<ItemModel>()
            {
                new ItemModel { Name = "Captain Underpants", Description="Need to read this one.", Value=4 },
                new ItemModel { Name = "Dogman", Description="A little silly to be assigned reading.", Value=5 },
                new ItemModel { Name = "Victor Von Vulture", Description="The leader of the Video Vultures", Value=2 },
                new ItemModel { Name = "President Penguin", Description="Upset about the demotion of Pluto's planetary status", Value=1 },
                new ItemModel { Name = "Major Monkey", Description="I can't remember what this guy's problem was", Value=5 },
                new ItemModel { Name = "Uncle Unicorn", Description="Go back to Uranus", Value=6 }
            };

            return true;
        }

        /// <summary>
        /// Add the data to the list
        /// </summary>
        /// <param name="data"></param>
        /// <returns>True for pass, else fail</returns>
        public async Task<bool> CreateAsync(ItemModel data)
        {
            datalist.Add(data);

            return await Task.FromResult(true);
        }

        /// <summary>
        /// Update the data with the information passed in
        /// </summary>
        /// <param name="data"></param>
        /// <returns>True for pass, else fail</returns>
        public async Task<bool> UpdateAsync(ItemModel data)
        {
            var oldData = datalist.Where((ItemModel arg) => arg.Id == data.Id).FirstOrDefault();
            datalist.Remove(oldData);
            datalist.Add(data);

            return await Task.FromResult(true);
        }

        /// <summary>
        /// Deletes the Data passed in by
        /// Removing it from the list
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True for pass, else fail</returns>
        public async Task<bool> DeleteAsync(string id)
        {
            var oldData = datalist.Where((ItemModel arg) => arg.Id == id).FirstOrDefault();
            datalist.Remove(oldData);

            return await Task.FromResult(true);
        }

        /// <summary>
        /// Takes the ID and finds it in the data set
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Record if found else null</returns>
        public async Task<ItemModel> ReadAsync(string id)
        {
            return await Task.FromResult(datalist.FirstOrDefault(s => s.Id == id));
        }

        /// <summary>
        /// Get the full list of data
        /// </summary>
        /// <param name="forceRefresh"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ItemModel>> IndexAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(datalist);
        }
    }
}