using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//파일의 입출력을 위한 네임스페이스
using System.IO;
//바이너리 파일포맷을 위한 네임스페이스
using System.Runtime.Serialization.Formatters.Binary;
//데이터 저장 클래스에 접근하기 위한 네임스페이스
using DataInfo;

public class DataManager : MonoBehaviour
{
    //파일이 저장될 물리적인 경로 및 파일명을 저장할 변수
    private string dataPath;
    //파일 저장 경로와 파일명 지정
    public void Initialize()
    {
        dataPath = Application.persistentDataPath + "/gameData.dat";
    }
    public void Save(GameData gameData)
    {
        //바이너리 파일 포맷을 위한 BinaryFormatter 생성
        BinaryFormatter bf = new BinaryFormatter();
        //데이터 저장을 위한 파일 생성
        FileStream file = File.Create(dataPath);

        //파일에 저장할 클래스에 데이터 할당
        GameData data = new GameData();
        data.killCount = gameData.killCount;
        data.hp = gameData.hp; ;
        data.speed = gameData.speed;
        data.damage = gameData.damage;
        data.equipItem = gameData.equipItem;

        //BinaryFormater를 사용해 파일에 데이터 기록
        bf.Serialize(file, data);
        file.Close();
    }

    public GameData Load()
    {
        if (File.Exists(dataPath))
        {
            //파일이 존재할 경우 데이터 불러오기
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(dataPath, FileMode.Open);
            GameData data = (GameData)bf.Deserialize(file);
            file.Close();

            return data;
        }
        else
        {
            //파일이 없을 경우 기본값을 반환
            GameData data = new GameData();
            return data;
        }
    }
}
