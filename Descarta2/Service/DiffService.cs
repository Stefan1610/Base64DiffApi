using Descarta2.Models;
using Descarta2.Repository;
using System.Text.RegularExpressions;

namespace Descarta2.Service
{
    public class DiffService
    {
        //Creating instance of repository
        private readonly DiffRepository _repository;

        public DiffService(DiffRepository diffRepository)
        {
            _repository = diffRepository;
        }
        //Method for comparing left and right data
        public async Task<JsonDiffDTO> Compare(int id)
        {
            DiffItemDTO itemLeft = await Select(id, diffPosition.Left);
            DiffItemDTO itemRight = await Select(id, diffPosition.Right);

            JsonDiffDTO result = new JsonDiffDTO();
            
            //returns exception if one of data is null
            if (itemLeft == null || itemRight == null)
            {
                throw new Exception("Not Found");
            }
            //If different size returns SizeDoNotMatch
            if (itemLeft.Data.Length != itemRight.Data.Length)
            {
                result.DiffResultType = DiffResultType.SizeDoNotMatch.ToString();
               
                return result;
            }
            //If datas are same returns Equal
            if(itemLeft.Data == itemRight.Data)
            {
                result.DiffResultType = DiffResultType.Equal.ToString();
               
                return result;
            }

            result.DiffResultType = DiffResultType.ContentDoNotMatch.ToString();
            List<Diff> diffList = new List<Diff>();
            byte[] LeftData = Convert.FromBase64String(itemLeft.Data);
            byte[] RightData = Convert.FromBase64String(itemRight.Data);
            int length = 0;
            int[] offset = new int[LeftData.Length];
                       
            for (int i = 0; i <= LeftData.Length; i++)
            {
                for (int j = 0; j < offset.Length; j++)
                {
                    if (i < LeftData.Length && LeftData[i] != RightData[i])
                    {
                        length = 0;
                       offset[j] = i;
                        while (i < LeftData.Length && LeftData[i] != RightData[i])
                        {
                            length++;
                            i++;
                        }
                        diffList.Add(new Diff(offset[j], length));
                    }
                  
                }
             }

            result.Diffs = diffList;
            return result;
        }
        //Method for saving/stroing data in "base"
        public async Task<bool> Save(DiffItemDTO itemdto)
        {
            string position = itemdto.Position == diffPosition.Left ? "L" : "R";
            DiffItem item = new DiffItem() { Id = itemdto.Id, Data = itemdto.Data, Position = position };

            try
            {
                if (await _repository.AddOrUpdate(item))
                    _repository.Save();

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(false);
            }
        }
        //Method for selecting item from base
        public async Task<DiffItemDTO> Select(int id, diffPosition position)
        {
            string _position = position == diffPosition.Left ? "L" : "R";
            return ConvertToDto(await _repository.Select(id, _position));
        }

        private DiffItemDTO ConvertToDto(DiffItem item)
        {
            if (item == null)
                return null;
            else
            {
                DiffItemDTO itemDto = new DiffItemDTO()
                {
                    Id = item.Id,
                    Data = item.Data,
                    Position = (item.Position == "L" ? diffPosition.Left : diffPosition.Right)
                };
                return itemDto;
            }
        }
    }
}
