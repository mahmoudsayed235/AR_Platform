using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using Newtonsoft.Json;

public class ReportBuilder : MonoBehaviour
{
    public class ReportRecord
    {
    }
    
    public class QuestionReportRecord : ReportRecord
    {
        [JsonProperty("questionID")]
        public int QuestionID { set; get; }
        [JsonProperty("choiceID")]
        public int ChoiceID { set; get; }
        [JsonProperty("attempt")]
        public int Attempt { set; get; }

        public QuestionReportRecord(int questionID, int choiceID, int attempt)
        {
            this.QuestionID = questionID;
            this.ChoiceID = choiceID;
            this.Attempt = attempt;
        }
    }

    public class DragQuestionReportRecord : ReportRecord
    {
        [JsonProperty("questionID")]
        public int QuestionID { set; get; }

        [JsonProperty("correctlyDraggedChoices")]
        public int CorrectlyDraggedChoices { set; get; }

        public DragQuestionReportRecord(int questionID, int correctlyDraggedChoices)
        {
            this.QuestionID = questionID;
            this.CorrectlyDraggedChoices = correctlyDraggedChoices;
        }
    }
    
    public class SimulationReport
    {
        [JsonProperty("simulationID")]
        public int SimulationID { set; get; }

        [JsonProperty("timeStamp")]
        public string TimeStamp { set; get; }

        [JsonProperty("reportRecords")]
        public List<ReportRecord> ReportRecords { set; get; }

        public SimulationReport(int simulationID)
        {
            this.SimulationID = simulationID;
            ReportRecords = new List<ReportRecord>();
        }
    }
    
    public class Report
    {
        [JsonProperty("simulationReports")]
        public List<SimulationReport> SimulationReports { set; get; }

        public Report()
        {
            SimulationReports = new List<SimulationReport>();
        }

        public void AddSimulationReport(int simulationID)
        {
            SimulationReports.Add(new SimulationReport(simulationID));
        }

        public void AddRecord(ReportRecord reportRecord)
        {
            SimulationReports[SimulationReports.Count - 1].ReportRecords.Add(reportRecord);
        }

        public void TimeStamp()
        {
            SimulationReports[SimulationReports.Count - 1].TimeStamp = System.DateTime.Now.ToString();
        }
    }
    
    private Report report;
    private int simulationID;

    private JsonSerializerSettings serializerSettings = new JsonSerializerSettings
    {
        TypeNameHandling = TypeNameHandling.Auto
    };


    void Awake()
    {
        simulationID = PlayerPrefs.GetInt(PlayerPrefsKeys.SimulationID, -1);

        if (simulationID == -1)
        {
            //Debug.LogError("Bad Simulation ID");
            return;
        }

        try
        {
            // initialize report
            report = JsonConvert.DeserializeObject<Report>(PlayerPrefs.GetString(PlayerPrefsKeys.QuestionsReport), serializerSettings);
        }
        catch(System.Exception e)
        {
            //Debug.LogErrorFormat("ReportBuilder -> Error: ", e.Message);
            report = new Report();
        }
        

        if (report == null)
        {
            report = new Report();
        }
        
        report.AddSimulationReport(simulationID);
    }

    private void OnEnable()
    {
        QuestionSystem.OnQuestionSolved += OnQuestionSolved;
        StyledQuestionSystem.OnQuestionSolved += OnQuestionSolved;
        DragQuestionManager.OnDragQuestionSolved += OnDragQuestionSolved;
    }

    private void OnDisable()
    {
        QuestionSystem.OnQuestionSolved -= OnQuestionSolved;
        StyledQuestionSystem.OnQuestionSolved -= OnQuestionSolved;
        DragQuestionManager.OnDragQuestionSolved -= OnDragQuestionSolved;
    }

    private void Stamp()
    {
        report.TimeStamp();
        PlayerPrefs.SetString(PlayerPrefsKeys.QuestionsReport, JsonConvert.SerializeObject(report, serializerSettings));
        PlayerPrefs.Save();
    }

    private void OnQuestionSolved(int questionID, int choiceID, int attempt, bool timeStamper)
    {
        report.AddRecord(new QuestionReportRecord(questionID, choiceID, attempt));

        if (timeStamper)
        {
            Stamp();
        }
    }

    private void OnDragQuestionSolved(int id, int correctChoices, bool timeStamper)
    {
        report.AddRecord(new DragQuestionReportRecord(id, correctChoices));

        if(timeStamper)
        {
            Stamp();
        }
    }
}
