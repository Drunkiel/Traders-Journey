using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadController : MonoBehaviour
{
    private string jsonSavePath;
    public DataToSave _data;

    void Awake()
    {
        jsonSavePath = Application.persistentDataPath + "/save.json";

        try
        {
            Load();
        }
        catch (System.Exception)
        {
            Save();
        }
    }

    public void Save()
    {
        //Creating file
        FileStream File1 = new FileStream(jsonSavePath, FileMode.OpenOrCreate);

        /*        //Data to save
                GameObject[] buildings = GameObject.FindGameObjectsWithTag("Building");

                _data._buildingsData.Clear();

                foreach (GameObject building in buildings)
                {
                    if (building.GetComponent<ProductionID>())
                    {
                        ProductionID _productionID = building.GetComponent<ProductionID>();

                        _data._buildingsData.Add(new BuildingData()
                        {
                            id = building.GetComponent<BuildingID>().id,
                            position = building.transform.position,
                            isInProduction = _productionID.isInProduction,
                            productionID = _productionID.productionID,
                            endTime = new EndTime()
                            {
                                years = _productionID.endTime.Year,
                                months = _productionID.endTime.Month,
                                days = _productionID.endTime.Day,
                                hours = _productionID.endTime.Hour,
                                minutes = _productionID.endTime.Minute,
                                seconds = _productionID.endTime.Second
                            }
                        });
                    }
                    else
                    {
                        _data._buildingsData.Add(new BuildingData()
                        {
                            id = building.GetComponent<BuildingID>().id,
                            position = building.transform.position
                        });
                    }
                }
        */

        if (CycleController.instance.week >= 1)
        {
            BuildingSystem _buildingSystem = BuildingSystem.instance;

            //For some reason I can not use only one pair of lists because saving to json overrides both savings as one
            List <Vector3> positions = new();
            List<string> _buildings = new();
            //So this is a reason why this list below exist
            List<Vector3> positions1 = new();
            List<string> _buildings1 = new();

            //Saving buildings
            if(_buildingSystem._allBuildings.Count > 0)
            {
                foreach (BuildingID _buildingID in _buildingSystem._allBuildings)
                {
                    positions.Add(_buildingID.transform.position);
                    _buildings.Add(_buildingID.buildingName);
                }
                _data._buildingSaveData.positions = positions;
                _data._buildingSaveData._buildingNames = _buildings;
            }

            //Saving paths
            if (_buildingSystem._allPaths.Count > 0)
            {
                _buildings1.Add(_buildingSystem._allPaths[0].buildingName); //Need only one path to define rest
                foreach (BuildingID _buildingID in _buildingSystem._allPaths)
                {
                    positions1.Add(_buildingID.transform.position);
                }

                _data._pathSaveData.positions = positions1;
                _data._pathSaveData._buildingNames = _buildings1;
            }
        }

        //Saving resources
        _data._playerSaveData = new()
        {
            populationQuantity = PlayerResources.populationQuantity,
            coinsQuantity = PlayerResources.coinsQuantity,
            suppliesQuantity = PlayerResources.suppliesQuantity,
            wheatQuantity = PlayerResources.wheatQuantity,
            meatQuantity = PlayerResources.meatQuantity,
            stoneQuantity = PlayerResources.stoneQuantity,
            goldQuantity = PlayerResources.goldQuantity,
            silverQuantity = PlayerResources.silverQuantity,
            diamondQuantity = PlayerResources.diamondQuantity,
        };

        //Saving data
        string jsonData = JsonUtility.ToJson(_data, true);

        File1.Close();
        File.WriteAllText(jsonSavePath, jsonData);
    }

    public void Load()
    {
        //Loading data
        string json = ReadFromFile();
        JsonUtility.FromJsonOverwrite(json, _data);

        /*        foreach (var data in _data._buildingsData)
                {
                    _buildingSystem.InitializeWithObject(_buildingSystem.buildingsPrefabs[data.id]);
                    _buildingSystem._objectToPlace.transform.position = data.position;
                    if (data.isInProduction)
                    {
                        _buildingSystem._objectToPlace.GetComponent<ProductionID>().isInProduction = data.isInProduction;
                        _buildingSystem._objectToPlace.GetComponent<ProductionID>().endTime = new System.DateTime(data.endTime.years, data.endTime.months, data.endTime.days, data.endTime.hours, data.endTime.minutes, data.endTime.seconds);
                    }
                    _buildingSystem.PlaceButton();
                }*/
    }

    private string ReadFromFile()
    {
        using (StreamReader Reader = new StreamReader(jsonSavePath))
        {
            string json = Reader.ReadToEnd();
            return json;
        }
    }
}
