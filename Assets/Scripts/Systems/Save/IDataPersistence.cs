using Systems.Save.Data;

namespace Systems.Save
{
    public interface IDataPersistence
    {
        public void LoadData(GameData data);

        public void SaveData(GameData data);
    }
}