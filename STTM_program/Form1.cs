using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace STTM_PJ01
{
    public partial class Form1 : Form
    {
        //input initial populaiton structure dataset path
        string city_file_path = null;
        //input population OD flow dataset path
        string odflow_file_dir_path = null;

        //output statistic results per day 
        string result_file_path = null;
        //output statistic results all day 
        string statistics_file = null;

        //parameters
        double exposedRate = 0.0;
        double infectedRate = 0.0;
        double e2i_rate = 0.0;
        double i2r_rate = 0.0;



        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Call the main function (with four paths above)
            cal_model(city_file_path, odflow_file_dir_path, result_file_path,statistics_file);
        }

        public void cal_model(string city_file_path, string odflow_file_dir_path, string result_file_path,string statistics_file)
        {
            string city_data_path = city_file_path;//initial populaiton structure dataset
            string odflow_data_path = odflow_file_dir_path;//population OD flow dataset

            int n = 0; //the day of daily statistic results(after population flow)
            int m = 0; //the day of daily statistic results(after population infection)

            DateTime dt = DateTime.Now;

            Dictionary<string, City> cityDic = new Dictionary<string, City>();//e.g.<A,City> represents city A and its properties

            StreamReader sr = new StreamReader(city_data_path);


            //read the population data for each city
            string line = null;
            string[] lines = null; //store initial data for each city temporarily e.g.[A,10000000,9999999,1,0,0]
            line = sr.ReadLine(); 
            while ((line = sr.ReadLine()) != null)
            {
                lines = line.Split(',');
                //add to cityDic<string, City>
                cityDic.Add(lines[0], new City(lines[0], Convert.ToDouble(lines[1]), Convert.ToDouble(lines[2]), Convert.ToDouble(lines[3]), Convert.ToDouble(lines[4]), Convert.ToDouble(lines[5])));
                //cityDic[lines[0]] represents a city, which can calculate the total number of infections in the city
                cityDic[lines[0]].Sum_patient.Add(cityDic[lines[0]].Pop_exposed + cityDic[lines[0]].Pop_infected + cityDic[lines[0]].Pop_recovered);
            }
            //-------------------------------brief summary-------------------------------
            //create cityDic used to store initial data
            //-------------------------------brief summary-------------------------------


            //-------------------------------A method of reading flow dataset sequentially-------------------------------

            DirectoryInfo dirInfo = new DirectoryInfo(odflow_file_dir_path);
            FileInfo[] theFiles = dirInfo.GetFiles();
            theFiles = theFiles.OrderBy(y => y.Name, new FileNameComparerClass()).ToArray();

            //-------------------------------A method of reading flow dataset sequentially-------------------------------

            int iii = 0;//accumulated days
            //iterate over the flow data
            foreach (var fileInfo in theFiles) 
            {
                Console.WriteLine("+++++++" + (++iii) + "++++++++"); 
                Console.WriteLine(fileInfo);
                Dictionary<string, ODFlow> odflowDic = new Dictionary<string, ODFlow>(); //population flow dice.g.<A_B,ODFlow>
                Dictionary<string, double> orate_Dic = new Dictionary<string, double>(); //people contact rate dic

                string temp_file_full_name = fileInfo.FullName;

                //read population flow data between all cities one day 
                sr = new StreamReader(temp_file_full_name);
                line = sr.ReadLine();
                while ((line = sr.ReadLine()) != null)
                {
                    lines = line.Split(',');
                    odflowDic.Add(string.Format($"{lines[0]}_{lines[1]}"), new ODFlow(lines[0], lines[1], Convert.ToDouble(lines[2]), Convert.ToDouble(lines[4])));

                    string tempKey = lines[0]; 

                    if (!orate_Dic.ContainsKey(tempKey))
                    {
                        orate_Dic.Add(tempKey, Convert.ToDouble(lines[4]));//e.g. [A,B,100,20200101,5,5]中的第5个
                    }
                }
                //Console.WriteLine("stop");

                //-------------------------------brief summary-------------------------------
                //create odflowDic and orate_Dic which are used to store the data for the day
                //-------------------------------brief summary-------------------------------


                //dic<key=O city ID> dic<key=D city ID> 
                Dictionary<string, List<string>> O_ODIDSetDic = new Dictionary<string, List<string>>();
                Dictionary<string, List<string>> D_ODIDSetDic = new Dictionary<string, List<string>>();

                //iterate over the key-value pairs odflowDic<string, ODFlow> e.g. {A_B,100}
                foreach (KeyValuePair<string, ODFlow> kvp in odflowDic)
                {
                    string ostr = kvp.Key.Split('_')[0]; //A_B --> A, B
                    string dstr = kvp.Key.Split('_')[1];

                    if (O_ODIDSetDic.ContainsKey(ostr)) //if cityID exists, then flow population add up
                    {
                        O_ODIDSetDic[ostr].Add(kvp.Key); //["A_B","A_C"...]
                    }
                    else //if cityID does not exists, then create a new list
                    {
                        List<string> list = new List<string>(); 
                        list.Add(kvp.Key); 
                        O_ODIDSetDic.Add(ostr, list);
                    }

                    if (D_ODIDSetDic.ContainsKey(dstr))
                    {
                        D_ODIDSetDic[dstr].Add(kvp.Key);
                    }
                    else
                    {
                        List<string> list = new List<string>();
                        list.Add(kvp.Key);
                        D_ODIDSetDic.Add(dstr, list);
                    }

                }

                //-------------------------------brief summary-------------------------------
                //{<A,["A_B","A_C","A_C"...]>,
                // <B,["B_A","B_C","B_D"...]>
                // ……                      }
                //To update each city's population inflow and outflow for the day
                //-------------------------------brief summary-------------------------------

                //Calculate the population flow data of S and E between all cities
                Dictionary<string, ODFlow_SE> ODFlow_SEDic = new Dictionary<string, ODFlow_SE>();//e.g.<"A_B",ODFlow_SE> 

                foreach (KeyValuePair<string, ODFlow> kvp in odflowDic)//e.g.odflowDic<A_B,ODFlow> 
                {
                    double S_OD = odflowDic[kvp.Key].Odcity_pop * cityDic[kvp.Value.Ocity_id].PopRate_susceptible_out; //A_B流动的总人数*S的比例=A_B流动的S的总人数
                    double E_OD = odflowDic[kvp.Key].Odcity_pop * cityDic[kvp.Value.Ocity_id].PopRate_exposed_out;//A_B流动的总人数*E的比例=A_B流动的E的总人数

                    ODFlow_SEDic.Add(kvp.Key, new ODFlow_SE(kvp.Value.Ocity_id, kvp.Value.Dcity_id, S_OD, E_OD, dt));//<"A_B",(A,B,100,50,1月1)>
                }

                //-------------------------------brief summary-------------------------------
                //create dic<ODFlow_SEDic> to store the S or E flowing from cityA to cityB,as for updating population structure data 
                //-------------------------------brief summary-------------------------------


                n += 1; //the day number for people flow
                string sta = result_file_path + '_' + n.ToString() + "flow.csv"; 
                StreamWriter swa = new StreamWriter(sta);
                swa.WriteLine("city_id,pop_sum,pop_susceptible,pop_exposed,pop_infected,pop_recovered");//ID|pop_sum|S|E|I|R
                foreach (KeyValuePair<string, City> kvp in cityDic) // e.g.<"A",(pop_sum,S,E,I,R……)>
                {
                    string cityID = kvp.Key;//e.g. "A"

                    List<string> od_idList = O_ODIDSetDic[cityID]; //e.g.assign <"A",["A_B","A_C","A_C"...]> to od_idList
                    double sum_susceptible_out = 0.0;
                    double sum_exposed_out = 0.0;
                    foreach (var item in od_idList) //e.g."A"
                    {

                        if (ODFlow_SEDic.ContainsKey(item)) 
                        {
                            double susceptible_out = ODFlow_SEDic[item].Susceptible_out; //s_out
                            double exposed_out = ODFlow_SEDic[item].Exposed_out;//e_out

                            sum_susceptible_out += susceptible_out; //s_out
                            sum_exposed_out += exposed_out; //e_out
                        }
                    }

                    od_idList = D_ODIDSetDic[cityID]; 
                    double sum_susceptible_in = 0.0;
                    double sum_exposed_in = 0.0;


                    foreach (var item in od_idList)
                    {
                        if (ODFlow_SEDic.ContainsKey(item))
                        {
                            double susceptible_in = ODFlow_SEDic[item].Susceptible_out;
                            double exposed_in = ODFlow_SEDic[item].Exposed_out;

                            sum_susceptible_in += susceptible_in;
                            sum_exposed_in += exposed_in;

 
                        }

                    }
                    //update Pop_sum, Pop_susceptible, Pop_exposed for each city 
                    kvp.Value.Pop_sum = kvp.Value.Pop_sum - (sum_susceptible_out + sum_exposed_out) + (sum_susceptible_in + sum_exposed_in);
                    kvp.Value.Pop_susceptible = kvp.Value.Pop_susceptible - sum_susceptible_out + sum_susceptible_in;
                    kvp.Value.Pop_exposed = kvp.Value.Pop_exposed - sum_exposed_out + sum_exposed_in;

                    //output
                    string str = string.Format($"{kvp.Value.City_id},{kvp.Value.Pop_sum},{kvp.Value.Pop_susceptible},{kvp.Value.Pop_exposed},{kvp.Value.Pop_infected},{kvp.Value.Pop_recovered}");
                    swa.WriteLine(str);

                }
                swa.Close();

                //-------------------------------brief summary-------------------------------
                //Output the population data of each city after daily flow
                //-------------------------------brief summary-------------------------------



                //updating infected numbers for each city
                foreach (KeyValuePair<string, City> kvp in cityDic)
                {
                    List<double> seir_list = process_of_infection(kvp.Value.Pop_sum, kvp.Value.Pop_susceptible, kvp.Value.Pop_exposed, kvp.Value.Pop_infected, kvp.Value.Pop_recovered, Convert.ToInt32(kvp.Key), orate_Dic[kvp.Key]);//orate_Dic[kvp.Key]
                    kvp.Value.Pop_susceptible = seir_list[0];
                    kvp.Value.Pop_exposed = seir_list[1];
                    kvp.Value.Pop_infected = seir_list[2];
                    kvp.Value.Pop_recovered = seir_list[3];
                    kvp.Value.Pop_sum = seir_list[0] + seir_list[1] + seir_list[2] + seir_list[3];

                    kvp.Value.Sum_patient.Add(kvp.Value.Pop_exposed + kvp.Value.Pop_infected + kvp.Value.Pop_recovered);

                    kvp.Value.PopRate_exposed_out = kvp.Value.Pop_exposed / (kvp.Value.Pop_susceptible + kvp.Value.Pop_exposed);
                    kvp.Value.PopRate_susceptible_out = kvp.Value.Pop_susceptible / (kvp.Value.Pop_susceptible + kvp.Value.Pop_exposed);

                }

                //-------------------------------brief summary-------------------------------
                //update the population structure data for each city, and use it as the initial data for the next iteration
                //-------------------------------brief summary-------------------------------


                m += 1;//the day number for infection
                string st = result_file_path + '_' + m.ToString() + ".csv"; 
                StreamWriter sw = new StreamWriter(st);
                sw.WriteLine("city_id,pop_sum,pop_susceptible,pop_exposed,pop_infected,pop_recovered");
                foreach (KeyValuePair<string, City> kvp in cityDic)
                {
                    string str = string.Format($"{kvp.Value.City_id},{kvp.Value.Pop_sum},{kvp.Value.Pop_susceptible},{kvp.Value.Pop_exposed},{kvp.Value.Pop_infected},{kvp.Value.Pop_recovered}");

                    sw.WriteLine(str);
                }
                sw.Close();

                //-------------------------------brief summary-------------------------------
                //output updated population data for each city in cityDic
                //-------------------------------brief summary-------------------------------

            }

            //------------------------------------------------Output statistics-------------------------------------------------------------
            int i = 0; //the number of days output
            string first_line = "city_id,end_s,end_e,end_i,end_r,in";
            for(; i <= Convert.ToInt32(m);i++)
            {
                first_line = first_line + "," + i.ToString(); //"city_id,end_s,end_e,end_i,end_r,in,0,1,2,3,4..."
            }
            StreamWriter sw1 = new StreamWriter(statistics_file); 

            sw1.WriteLine(first_line);
            foreach (KeyValuePair<string, City> kvp in cityDic)
            {
                string line_file = string.Format($"{kvp.Value.City_id},{kvp.Value.Pop_susceptible},{kvp.Value.Pop_exposed},{kvp.Value.Pop_infected},{kvp.Value.Pop_recovered},{kvp.Value.Pop_sum}"); ;
                foreach(double v in kvp.Value.Sum_patient)
                {
                    line_file = line_file + "," + string.Format($"{v}"); //"id,s,e,i,r,1,2,3,4..."
                }
                sw1.WriteLine(line_file);
            }
            sw1.Close();

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            checkBox_synthetic.Checked = true;

            if (checkBox_synthetic.Checked)
            {
                tb_exposedRate.Text = 0.06.ToString();
                tb_infectedRate.Text = 0.02.ToString();
                tb_e2i_rate.Text = 0.142857.ToString();
                tb_i2r_rate.Text = 0.142857.ToString();

                //input initial populaiton structure dataset path(SyntheticData)
                city_file_path = string.Format(@"{0}\{1}", Application.StartupPath, @"ExperimentData\SyntheticData\Pop_StructureData\U1.csv");
                //input population OD flow dataset path(SyntheticData)
                odflow_file_dir_path = string.Format(@"{0}\{1}", Application.StartupPath, @"ExperimentData\SyntheticData\Pop_OD_FlowData");

                //output statistic results per day(SyntheticData)
                result_file_path = string.Format(@"{0}\{1}", Application.StartupPath, @"ExperimentData\SyntheticData\Result\Daily_result\result");
                //output statistic results all day(SyntheticData)
                statistics_file = string.Format(@"{0}\{1}", Application.StartupPath, @"ExperimentData\SyntheticData\Result\Statistic_result\statistic_result.csv");

                tb_pop_structure_data.Text = city_file_path;
                tb_pop_flow_data.Text = odflow_file_dir_path;

                tb_dailyResult.Text = result_file_path;
                tb_statisticResult.Text = statistics_file;

            }
            else
            {
                tb_exposedRate.Text = (3 * 3.430 / (7 * 4 * 15.18042278)).ToString();
                tb_infectedRate.Text = (3.430 / (7 * 4 * 15.18042278)).ToString();
                tb_e2i_rate.Text = 0.1428570.ToString();
                tb_i2r_rate.Text = 0.1428570.ToString();

                //input initial populaiton structure dataset path(RealworldData)
                city_file_path = string.Format(@"{0}\{1}", Application.StartupPath, @"ExperimentData\RealworldData\Pop_StructureData\U1.csv");
                //input population OD flow dataset path(RealworldData)
                odflow_file_dir_path = string.Format(@"{0}\{1}", Application.StartupPath, @"ExperimentData\RealworldData\Pop_OD_FlowData");

                //output statistic results per day(RealworldData)
                result_file_path = string.Format(@"{0}\{1}", Application.StartupPath, @"ExperimentData\RealworldData\Result\Daily_result\result");
                //output statistic results all day(RealworldData)
                statistics_file = string.Format(@"{0}\{1}", Application.StartupPath, @"ExperimentData\RealworldData\Result\Statistic_result\statistic_result.csv");

                tb_pop_structure_data.Text = city_file_path;
                tb_pop_flow_data.Text = odflow_file_dir_path;

                tb_dailyResult.Text = result_file_path;
                tb_statisticResult.Text = statistics_file;
            }
        }

        //epidemic dynamic model
        public List<double> process_of_infection(double Pop_sum_t1, double Pop_susceptible_t1, double Pop_exposed_t1, double Pop_infected_t1, double Pop_recovered_t1, int city, double PopRate_daily_contact_t1)
        {
            //SyntheticData
            double e1 = exposedRate;
            double e2 = infectedRate;
            double i1 = e2i_rate;//e2i
            double r1 = i2r_rate;//i2r
            

            List<double> t2 = new List<double>();
            double Pop_susceptible_t2 = Pop_susceptible_t1 - e1 * PopRate_daily_contact_t1 * Pop_exposed_t1 / (Pop_sum_t1 - Pop_recovered_t1) * Pop_susceptible_t1 - e2 * PopRate_daily_contact_t1 * Pop_infected_t1 / (Pop_sum_t1 - Pop_recovered_t1) * Pop_susceptible_t1;
            double Pop_exposed_t2 = Pop_exposed_t1 - i1 * Pop_exposed_t1 + e1 * PopRate_daily_contact_t1 * Pop_exposed_t1 / (Pop_sum_t1 - Pop_recovered_t1) * Pop_susceptible_t1 + e2 * PopRate_daily_contact_t1 * Pop_infected_t1 / (Pop_sum_t1 - Pop_recovered_t1) * Pop_susceptible_t1;
            double Pop_infected_t2 = Pop_infected_t1 + i1 * Pop_exposed_t1 - r1 * Pop_infected_t1;
            double Pop_recovered_t2 = Pop_recovered_t1 + r1 * Pop_infected_t1;

            Pop_susceptible_t1 = Pop_susceptible_t2;
            Pop_exposed_t1 = Pop_exposed_t2;
            Pop_infected_t1 = Pop_infected_t2;
            Pop_recovered_t1 = Pop_recovered_t2;

            t2.Add(Pop_susceptible_t1);
            t2.Add(Pop_exposed_t1);
            t2.Add(Pop_infected_t1);
            t2.Add(Pop_recovered_t1);
            return t2;


        }

        [System.Runtime.InteropServices.DllImport("Shlwapi.dll", CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
        public static extern int StrCmpLogicalW(string psz1, string psz2);
        public class FileNameComparerClass : IComparer<string>
        {
            [System.Runtime.InteropServices.DllImport("Shlwapi.dll", CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
            public static extern int StrCmpLogicalW(string psz1, string psz2);
            public int Compare(string psz1, string psz2)
            {
                return StrCmpLogicalW(psz1, psz2);
            }
        }

        private void btn_Click_close(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox_synthetic_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_synthetic.Checked)
            {
                checkBox_realworld.Checked = false;

                tb_exposedRate.Text = 0.06.ToString();
                tb_infectedRate.Text = 0.02.ToString();
                tb_e2i_rate.Text = 0.142857.ToString();
                tb_i2r_rate.Text = 0.142857.ToString();

                exposedRate = 0.06;
                infectedRate = 0.02;
                e2i_rate = 0.142857;
                i2r_rate = 0.142857;


                //input initial populaiton structure dataset path(SyntheticData)
                city_file_path = string.Format(@"{0}\{1}", Application.StartupPath, @"ExperimentData\SyntheticData\Pop_StructureData\U1.csv");
                //input population OD flow dataset path(SyntheticData)
                odflow_file_dir_path = string.Format(@"{0}\{1}", Application.StartupPath, @"ExperimentData\SyntheticData\Pop_OD_FlowData");

                //output statistic results per day(SyntheticData)
                result_file_path = string.Format(@"{0}\{1}", Application.StartupPath, @"ExperimentData\SyntheticData\Result\Daily_result\result");
                //output statistic results all day(SyntheticData)
                statistics_file = string.Format(@"{0}\{1}", Application.StartupPath, @"ExperimentData\SyntheticData\Result\Statistic_result\statistic_result.csv");


                tb_pop_structure_data.Text = city_file_path;
                tb_pop_flow_data.Text = odflow_file_dir_path;

                tb_dailyResult.Text = result_file_path;
                tb_statisticResult.Text = statistics_file;
            }
            else
            {
                checkBox_realworld.Checked = true;

                tb_exposedRate.Text = (3 * 3.430 / (7 * 4 * 15.18042278)).ToString();
                tb_infectedRate.Text = (3.430 / (7 * 4 * 15.18042278)).ToString();
                tb_e2i_rate.Text = 0.1428571.ToString();
                tb_i2r_rate.Text = 0.1428571.ToString();

                exposedRate = 3 * 3.430 / (7 * 4 * 15.18042278);
                infectedRate = 3.430 / (7 * 4 * 15.18042278);
                e2i_rate = 0.142857;
                i2r_rate = 0.142857;

                //input initial populaiton structure dataset path(RealworldData)
                city_file_path = string.Format(@"{0}\{1}", Application.StartupPath, @"ExperimentData\RealworldData\Pop_StructureData\U1.csv");
                //input population OD flow dataset path(RealworldData)
                odflow_file_dir_path = string.Format(@"{0}\{1}", Application.StartupPath, @"ExperimentData\RealworldData\Pop_OD_FlowData");

                //output statistic results per day(RealworldData)
                result_file_path = string.Format(@"{0}\{1}", Application.StartupPath, @"ExperimentData\RealworldData\Result\Daily_result\result");
                //output statistic results all day(RealworldData)
                statistics_file = string.Format(@"{0}\{1}", Application.StartupPath, @"ExperimentData\RealworldData\Result\Statistic_result\statistic_result.csv");


                tb_pop_structure_data.Text = city_file_path;
                tb_pop_flow_data.Text = odflow_file_dir_path;

                tb_dailyResult.Text = result_file_path;
                tb_statisticResult.Text = statistics_file;
            }
        }

        private void checkBox_realworld_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_realworld.Checked)
            {
                checkBox_synthetic.Checked = false;
            }
            else
            {
                checkBox_synthetic.Checked = true;
            }
        }

        private void tb_pop_structure_data_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
    /// <summary>
    /// Origin-destination metrix of population(S and E) between two any cities.
    /// </summary>
    public class ODFlow_SE
    {
        private string ocity_id;
        private string dcity_id;
        private double susceptible_out;
        private double exposed_out;
        private DateTime datetime;

        public ODFlow_SE(string ocity_id, string dcity_id, double susceptible_out, double exposed_out, DateTime datetime)
        {
            this.Ocity_id = ocity_id;
            this.Dcity_id = dcity_id;
            this.Susceptible_out = susceptible_out;
            this.Exposed_out = exposed_out;
            this.datetime = datetime;
        }

        /// <summary>
        /// Origin city id
        /// </summary>
        public string Ocity_id { get => ocity_id; set => ocity_id = value; }
        /// <summary>
        /// Destination city ID
        /// </summary>
        public string Dcity_id { get => dcity_id; set => dcity_id = value; }
        /// <summary>
        /// Susceptiable population from orgin city to destination city
        /// </summary>
        public double Susceptible_out { get => susceptible_out; set => susceptible_out = value; }
        /// <summary>
        /// Exposed population from orgin city to destination city
        /// </summary>
        public double Exposed_out { get => exposed_out; set => exposed_out = value; }
        /// <summary>
        /// date time of flow
        /// </summary>
        public DateTime Datetime { get => datetime; set => datetime = value; }
    }

    /// <summary>
    /// Origin-destination metrix of population between two any cities.
    /// </summary>
    public class ODFlow
    {
        private string ocity_id;
        private string dcity_id;
        private double odcity_pop;
        private double o_rate;

        public ODFlow(string ocity_id, string dcity_id, double odcity_pop, double o_rate)
        {
            this.ocity_id = ocity_id;
            this.dcity_id = dcity_id;
            this.odcity_pop = odcity_pop;
            this.o_rate = o_rate;
        }
        /// <summary>
        /// Origin city ID
        /// </summary>
        public string Ocity_id { get => ocity_id; set => ocity_id = value; }
        /// <summary>
        /// Destination city ID
        /// </summary>
        public string Dcity_id { get => dcity_id; set => dcity_id = value; }
        /// <summary>
        /// Population from orgin city to destination city
        /// </summary>
        public double Odcity_pop { get => odcity_pop; set => odcity_pop = value; }

        public double O_rate { get => o_rate; set => o_rate = value; }
    }

    /// <summary>
    /// Population structure of city
    /// </summary
    public class City
    {
        private string city_id;
        private double pop_sum;
        private double pop_susceptible;
        private double pop_exposed;
        private double pop_infected;
        private double pop_recovered;
        private List<double> sum_patient = new List<double>();

        private double popRate_susceptible_out;//the flow out percentage of S 
        private double popRate_exposed_out;//the flow out percentage of E

        public City(string city_id, double pop_sum, double pop_susceptible, double pop_exposed, double pop_infected, double pop_recovered)
        {
            this.city_id = city_id;
            this.pop_sum = pop_sum;
            this.pop_susceptible = pop_susceptible;
            this.pop_exposed = pop_exposed;
            this.pop_infected = pop_infected;
            this.pop_recovered = pop_recovered;

            this.PopRate_susceptible_out = this.pop_susceptible / (this.pop_susceptible + this.pop_exposed);
            this.PopRate_exposed_out  = this.pop_exposed / (this.pop_susceptible + this.pop_exposed);
        }

        /// <summary>
        /// City ID
        /// </summary>
        public string City_id { get => city_id; set => city_id = value; }
        /// <summary>
        /// Total population in this city
        /// </summary>
        public double Pop_sum { get => pop_sum; set => pop_sum = value; }
        /// <summary>
        /// Total susceptible population in this city
        /// </summary>
        public double Pop_susceptible { get => pop_susceptible; set => pop_susceptible = value; }
        /// <summary>
        /// Total exposed population in this city
        /// </summary>
        public double Pop_exposed { get => pop_exposed; set => pop_exposed = value; }
        /// <summary>
        /// Total infected population in this city
        /// </summary>
        public double Pop_infected { get => pop_infected; set => pop_infected = value; }
        /// <summary>
        /// Total recovered population in this city
        /// </summary>
        public double Pop_recovered { get => pop_recovered; set => pop_recovered = value; }
        /// <summary>
        /// Total patients in this city
        /// </summary>
        public List<double> Sum_patient { get => sum_patient; set => sum_patient = value; }
        /// <summary>
        /// Out population rate of susceptible from this city  
        /// </summary>
        public double PopRate_susceptible_out { get => popRate_susceptible_out; set => popRate_susceptible_out = value; }
        /// <summary>
        /// Out population rate of exposed from this city  
        /// </summary>
        public double PopRate_exposed_out { get => popRate_exposed_out; set => popRate_exposed_out = value; }
    }



}
