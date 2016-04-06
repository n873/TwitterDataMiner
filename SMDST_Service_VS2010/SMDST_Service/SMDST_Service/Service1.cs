using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Twitterizer;
using System.Xml;
using System.IO;
using System.Net;
using System.Threading;
using System.Web;

namespace SMDST_Service
{
    public partial class Service1 : ServiceBase
    {
        ArrayList minList = new ArrayList();
        int requestRate = 0;
        string[] journalist = new string[] 
                    {   
                        //Does not exits - "gertvdwesth","eduanroos"
                        //----------------------------------------------------------------------------
                        "Nelliebrand","alet87","gerbjan","carrynann","Blommelief",
                        "MaygeneD","anneliejm","RozanneEls","waldimar","Gavin_Prins",
                        "AdriaanBasson","carienduplessis","cathydlodlo","sa_poptart","CharlduPlessc",
                        "DanieMothowagae","TheGabi","SpeedQueen","kaymorizm","LoyisoSidimba",
                        "MandyRossouw","Mokgads","Bhintsintsi","vercingetorics","S_samaYende",
                        "TimSpiritMolobi","mbanjwax","zinhlemap","ferialhaffajee","City_Press",
                        "volksbladnuus","Sonkoerant","Sondagkoerant","Koerant_Rapport","dailysunsa",
                        "Beeld_Nuus","Die_Burger","Sport24Guy","Sport24Rugby","skuimkop24",
                        "altusmomberg","Stephen_Nell","Leighton_K","dmjoubert","groensake","Vida1b",
                        "elmakloppers","lmarib","janniedelange","saaymancarolien","karlakosklets",
                        "lientjieM","groenc","Samarie_sam","animvw","athi_saba",
                        "Sport24Team","Sport24Rugby‎","christasmuts","rudolphlake","diemol24",
                        "marcobotha","kobuspretorius1","ssharim","gkarstens","johanndejager","jorries1982",
                        "BabsShota","DumisaneLubisi","Fikelelo","GayleMahala","hermanverwey",
                        "LesLehM","lieslpret","Azanai","Leosejake","LubabaloNgcukan",
                        "nxumalo4","MawandeMvumvu","_M_N_M_","TashJoeZA","NickiGules",
                        "Percy_Mabandu","S_samaYende","JayJNia","vilakazim","annerientjie"
                    };

        public Service1()
        {
            InitializeComponent();
            OnStart(journalist);
        }

        public void OnDebug()
        {
            OnStart(null);
        }

        protected override void OnStart(string[] args)
        {
            File.Create(AppDomain.CurrentDomain.BaseDirectory + "Onstart.txt");
            userTweets t1 = new userTweets();
            OAuthTokens tokens = new OAuthTokens();
            directoryCheck dc = new directoryCheck();
            tokens.ConsumerKey = "OOFHHg7ZtaeEbFmnpDBBA";
            tokens.ConsumerSecret = "EIwCc9QhinfS74e4CC80ReieRXBTf3IegcjLRvIuU";
            tokens.AccessToken = "42821855-d1zis0eraGmHLPAPWUtUEVT6KaGe107917hT1x96k";
            tokens.AccessTokenSecret = "AyiEFOjy0v3lFE1NSBLSHQpNbyulmDn38ER3c2Bzo";
            dc.checkDirectories();

            t1.getTweets(journalist, tokens, requestRate);
            OnStop();
        }

        protected override void OnStop()
        {
            File.Create(AppDomain.CurrentDomain.BaseDirectory + "Onstop.txt");
            Thread.Sleep(new TimeSpan(22, 0, 0));
            OnStart(journalist);
        }
    }
}
