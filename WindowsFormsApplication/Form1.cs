using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApplication5
{
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();

            // elevator 1 is the elevator on the left, while the 2nd elevator is on the right
            elevator2arrival.Visible = false;// the picturebox with the elevator gif image 1
            elevator1arrival.Visible = false;//for the second

            floor0l1.Load("http://i.imgur.com/NhZPt6q.png");// the 1st elevator light on the ground floor is turned on, because they are there at the start
            floor1l1.Load("http://i.imgur.com/8Vws7O1.png");//rest lights are turned off
            floor2l1.Load("http://i.imgur.com/8Vws7O1.png");
            floor3l1.Load("http://i.imgur.com/8Vws7O1.png");
            floor4l1.Load("http://i.imgur.com/8Vws7O1.png");

            floor0l2.Load("http://i.imgur.com/NhZPt6q.png");//lights for the second elevator
            floor1l2.Load("http://i.imgur.com/8Vws7O1.png");
            floor2l2.Load("http://i.imgur.com/8Vws7O1.png");
            floor3l2.Load("http://i.imgur.com/8Vws7O1.png");
            floor4l2.Load("http://i.imgur.com/8Vws7O1.png");
        }

        #region General
        public static int[] floors1 = { 0, 1, 2, 3, 4 };// a matrix that will save the floors where the elevator is located
        public static int[] floors2 = { 0, 1, 2, 3, 4 };
        int[,] loc1 = new int[5, 2];// the matrix with the elevator Gif locations
        int[,] loc2 = new int[5, 2];
        public static int elevfl1;// the variable to save on which floor the elevator is located
        public static int elevfl2;

        
        // this 2D matrix will initially save the 1st floor of the call floor
        // and in the 2nd dimension of how many repetitions each call awaits
        // size 13 whatever buttons in the application
        public static int[,] call1 = new int[,] { { -1, 0 },// initialize with -1 floors since there are no calls
            { -1, 0 },
            { -1, 0 },
            { -1, 0 },
            { -1, 0 },
            { -1, 0 },
            { -1, 0 },
            { -1, 0 },
            { -1, 0 },
            { -1, 0 },
            { -1, 0 },
            { -1, 0 },
            { -1, 0 } };
        
        
        public static int[,] call2 = new int[,] { { -1, 0 },
            { -1, 0 },
            { -1, 0 },
            { -1, 0 },
            { -1, 0 },
            { -1, 0 },
            { -1, 0 },
            { -1, 0 },
            { -1, 0 },
            { -1, 0 },
            { -1, 0 },
            { -1, 0 },
            { -1, 0 } };
        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            // initialize the locations where the elevator gif will appear
            // for the 1st elevator
            loc1[0, 0] = 168 + 74;//the X
            loc1[0, 1] = 554;//the Y
            loc1[1, 0] = 168 + 74;
            loc1[1, 1] = 435;
            loc1[2, 0] = 168 + 74;
            loc1[2, 1] = 317;
            loc1[3, 0] = 168 + 74;
            loc1[3, 1] = 200;
            loc1[4, 0] = 168 + 74;
            loc1[4, 1] = 80;

            //for the 2nd elevator
            loc2[0, 0] = 306 + 74;//the X
            loc2[0, 1] = 554;//the Y
            loc2[1, 0] = 306 + 74;
            loc2[1, 1] = 435;
            loc2[2, 0] = 306 + 74;
            loc2[2, 1] = 317;
            loc2[3, 0] = 306 + 74;
            loc2[3, 1] = 200;
            loc2[4, 0] = 306 + 74;
            loc2[4, 1] = 80;

            // we initialize the elevators on the ground floor
            elevator1arrival.Location = new Point(loc1[0, 0], loc1[0, 1]);
            elevfl1 = floors1[0];

            elevator2arrival.Location = new Point(loc2[0, 0], loc2[0, 1]);
            elevfl2 = floors2[0];

            callchecker1.Start();// start the check procedure for a call
            callchecker2.Start();
        }

        private void button13_Click(object sender, EventArgs e)//Simulator Info Button
        {
            MessageBox.Show("This is the completed version of the monthly project" + "\n" +
                "the elevators works properly, it takes 2 seconds to move at each floor and 5 seconds to open the door and play the audio file" + "\n" +
                "the elevators will answer all calls, so if for a call it takes 12 or more repeats to answer it, " + "\n" +
                "because of a call loop, the elevators will go answer it immediately" 
                , "Simulator's Information");
        }

        WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();// with the help of windows media player i will play the 2 sounds of elevator door opening
        WMPLib.WindowsMediaPlayer wplayer2 = new WMPLib.WindowsMediaPlayer();// create windows media player object 2 times

        private void timerarrival1_Tick(object sender, EventArgs e)// in this Timer I will display the Gif of the 1st elevator
        {//5 δευτερόλεπτα
            
            elevator1arrival.Visible = false;
            timerarrival1.Stop();
        }
        private void timerarrival2_Tick(object sender, EventArgs e)//for the 2nd elevator
        {
            
            elevator2arrival.Visible = false;

            timerarrival2.Stop();
        }
   

        public static int flag1 = -1;// Pointers showing how many calls there are for the 1st elevator
        public static int flag2 = -1;// for the 2nd

        #region Buttons1
        // pressing buttons call method and pass a value, the call-button floor
        // these keys are the buttons inside the 1st elevator
        private void elevator1f0_Click(object sender, EventArgs e)
        {
            Queue1(0);//call the method
            elevator1f0.Enabled = false;// you can't make the same call again until it is answered
        }

        private void elevator1f1_Click(object sender, EventArgs e)
        {
            Queue1(1);
            elevator1f1.Enabled = false;
        }

        private void elevator1f2_Click(object sender, EventArgs e)
        {
            Queue1(2);
            elevator1f2.Enabled = false;
        }

        private void elevator1f3_Click(object sender, EventArgs e)
        {
            Queue1(3);
            elevator1f3.Enabled = false;
        }

        private void elevator1f4_Click(object sender, EventArgs e)
        {
            Queue1(4);
            elevator1f4.Enabled = false;
        }
        #endregion

        #region Buttons2
        // these are the buttons inside the 2nd elevator
        // call a 2nd method and pass the floor value
        private void elevator2f0_Click_1(object sender, EventArgs e)
        {
            elevator2f0.Enabled = false;
            Queue2(0);
        }

        private void elevator2f1_Click_1(object sender, EventArgs e)
        {
            elevator2f1.Enabled = false;
            Queue2(1);
        }

        private void elevator2f2_Click_1(object sender, EventArgs e)
        {
            elevator2f2.Enabled = false;
            Queue2(2);
        }

        private void elevator2f3_Click_1(object sender, EventArgs e)
        {
            elevator2f3.Enabled = false;
            Queue2(3);
        }

        private void elevator2f4_Click_1(object sender, EventArgs e)
        {
            elevator2f4.Enabled = false;
            Queue2(4);
        }

        #endregion

        #region Floor Buttons

        // the buttons on the floors
        // I'll check which elevator is closer to the call or if the 1st elevator is as close as the 2nd it goes by default
        // and then call the corresponding method
        private void floor0bu_Click(object sender, EventArgs e)
        {
            if(elevfl1 == elevfl2 && elevfl1 == 0)// if the elevators are on the same floor
            {
                Queue1(0);
                floor0bu.Enabled = false;
            }
            else if(Math.Abs(elevfl1 - 0) <= Math.Abs(elevfl2 - 0))// if the 1st elevator is closer or if the 2 elevators are even closer, the 1st will answer the call
            {
                Queue1(0);
                floor0bu.Enabled = false;
            }
            else if(Math.Abs(elevfl1 - 0) > Math.Abs(elevfl2 - 0))// if the 2nd elevator is closer then this will answer the call
            {
                Queue2(0);
                floor0bu.Enabled = false;
            }
        }

        private void floor1bd_Click(object sender, EventArgs e)
        {
            if (elevfl1 == elevfl2 && elevfl1 == 1)
            {
                Queue1(1);
                floor1bd.Enabled = false;
            }
            else if (Math.Abs(elevfl1 - 1) <= Math.Abs(elevfl2 - 1))
            {
                Queue1(1);
                floor1bd.Enabled = false;
            }
            else if (Math.Abs(elevfl1 - 1) > Math.Abs(elevfl2 - 1))
            {
                Queue2(1);
                floor1bd.Enabled = false;
            }
        }

        private void floor1bu_Click(object sender, EventArgs e)
        {
            if (elevfl1 == elevfl2 && elevfl1 == 1)
            {
                Queue1(1);
                floor1bu.Enabled = false;
            }
            else if (Math.Abs(elevfl1 - 1) <= Math.Abs(elevfl2 - 1))
            {
                Queue1(1);
                floor1bu.Enabled = false;
            }
            else if (Math.Abs(elevfl1 - 1) > Math.Abs(elevfl2 - 1))
            {
                Queue2(1);
                floor1bu.Enabled = false;
            }
        }

        private void floor2bd_Click(object sender, EventArgs e)
        {
            if (elevfl1 == elevfl2 && elevfl1 == 2)
            {
                Queue1(2);
                floor2bd.Enabled = false;
            }
            else if (Math.Abs(elevfl1 - 2) <= Math.Abs(elevfl2 - 2))
            {
                Queue1(2);
                floor2bd.Enabled = false;
            }
            else if (Math.Abs(elevfl1 - 2) > Math.Abs(elevfl2 - 2))
            {
                Queue2(2);
                floor2bd.Enabled = false;
            }
        }

        private void floor2bu_Click(object sender, EventArgs e)
        {
            if (elevfl1 == elevfl2 && elevfl1 == 2)
            {
                Queue1(2);
                floor2bu.Enabled = false;
            }
            else if (Math.Abs(elevfl1 - 2) <= Math.Abs(elevfl2 - 2))
            {
                Queue1(2);
                floor2bu.Enabled = false;
            }
            else if (Math.Abs(elevfl1 - 2) > Math.Abs(elevfl2 - 2))
            {
                Queue2(2);
                floor2bu.Enabled = false;
            }
        }

        private void floor3bd_Click(object sender, EventArgs e)
        {
            if (elevfl1 == elevfl2 && elevfl1 == 3)
            {
                Queue1(3);
                floor3bd.Enabled = false;
            }
            else if (Math.Abs(elevfl1 - 3) <= Math.Abs(elevfl2 - 3))
            {
                Queue1(3);
                floor3bd.Enabled = false;
            }
            else if (Math.Abs(elevfl1 - 3) > Math.Abs(elevfl2 - 3))
            {
                Queue2(3);
                floor3bd.Enabled = false;
            }
        }

        private void floor3bu_Click(object sender, EventArgs e)
        {
            if (elevfl1 == elevfl2 && elevfl1 == 3)
            {
                Queue1(3);
                floor3bu.Enabled = false;
            }
            else if (Math.Abs(elevfl1 - 3) <= Math.Abs(elevfl2 - 3))
            {
                Queue1(3);
                floor3bu.Enabled = false;
            }
            else if (Math.Abs(elevfl1 - 3) > Math.Abs(elevfl2 - 3))
            {
                Queue2(3);
                floor3bu.Enabled = false;
            }
        }

        private void floor4bd_Click(object sender, EventArgs e)
        {
            if (elevfl1 == elevfl2 && elevfl1 == 4)
            {
                Queue1(4);
                floor4bd.Enabled = false;
            }
            else if (Math.Abs(elevfl1 - 4) <= Math.Abs(elevfl2 - 4))
            {
                Queue1(4);
                floor4bd.Enabled = false;
            }
            else if (Math.Abs(elevfl1 - 4) > Math.Abs(elevfl2 - 4))
            {
                Queue2(4);
                floor4bd.Enabled = false;
            }
        }


        #endregion


        public void Queue1(int x)// this is the method that makes the order of calls when they are made
        {// x is the floor of the call
            
            int a, b;

            call1[flag1 + 1, 0] = x;
            if (flag1 != -1)//if not 1st call
            {
                for (int i = flag1 + 1; i >= 1; i--)// check for calls that exist
                {
                    if (Math.Abs(call1[i - 1, 0] - elevfl1) > Math.Abs(call1[i, 0] - elevfl1))
                    {// if the call floor is closer than the floor of the previous call
                        // these 2 calls exchange location
                        a = call1[i - 1, 0];
                        call1[i - 1, 0] = call1[i, 0];
                        call1[i, 0] = a;
                        b = call1[i - 1, 1];
                        call1[i - 1, 1] = call1[i, 1];
                        call1[i, 1] = b;
                    }
                    else if (call1[i, 0] == call1[i - 1, 0])
                    {// if the calls are from the same floor (other button) count as one
                      // so nothing happens
                    }
                    else//else at next iteration
                        break;
                }
                for (int i = 0; i <= flag1 + 1; i++)
                {// here I will check if there are calls on the same floor and delete the extra calls
                    if (call1[i, 0] == call1[i + 1, 0])
                    {
                        int z, o;
                        for (int j = i; i <= 11; i++)
                        {
                            z = call1[i + 1, 0];
                            o = call1[i + 1, 1];
                            call1[i, 0] = z;
                            call1[i, 1] = o;
                        }
                        flag1--;
                    }
                }
            }
            flag1++;

            printer();// show the calls in order
        }

        public void Queue2(int x)// this is the same method as Queue1 for the 2nd elevator call list
        {
            int a, b;

            if (flag2 == -1)
            {
                call2[flag2 + 1, 0] = x;
                flag2++;
            }
            else if (flag2 <= 12 && flag2 != -1)
            {
                call2[flag2 + 1, 0] = x;


                for (int i = flag2 + 1; i >= 1; i--)
                {
                    if (Math.Abs(call2[i - 1, 0] - elevfl2) > Math.Abs(call2[i, 0] - elevfl2))
                    { 
                        
                        a = call2[i - 1, 0];
                        call2[i - 1, 0] = call2[i, 0];
                        call2[i, 0] = a;
                        b = call2[i - 1, 1];
                        call2[i - 1, 1] = call2[i, 1];
                        call2[i, 1] = b;
                    }
                    else if (call2[i, 0] == call2[i - 1, 0])
                    {
                       
                    }
                    else
                        break;
                }
                for (int i = 0; i <= flag2 + 1; i++)
                {
                    if (call2[i, 0] == call2[i + 1, 0])
                    {
                        int z, o;
                        for (int j = i; i <= 11; i++)
                        {
                            z = call2[i + 1, 0];
                            o = call2[i + 1, 1];
                            call2[i, 0] = z;
                            call2[i, 1] = o;
                        }
                        flag2--;
                    }
                }
                flag2++;
            }
            printer();
        }

        private void callchecker1_Tick(object sender, EventArgs e)// timer that checks if there is a 1st call in the call list
        {
            if (call1[0, 0] != -1 && !timerarrival1.Enabled )
            {
                movement1.Start();// the timer in which they occur during the elevator movement
            }
        }

        // its name is movement1, it doesn't look like I renamed and was not renewed
        private void movement_Tick(object sender, EventArgs e)// the timer in which they occur during the elevator movement
        {//example the change of floors, the lighting of lamps, etc.

            Check_Waits1(call1);// method to check if there is a call waiting for more than 10 repetitions

            Turn_Lights1(elevfl1);// method of lighting the lamps
            if (call1[0, 0] == elevfl1)// if the 1st call is on the same floor as the elevator
            {
                movement1.Stop();//stop the procedure
                elevator1arrival.Location = new Point(loc1[call1[0, 0], 0], loc1[call1[0, 0], 1]);// move the gif pictureBox
                elevator1arrival.Visible = true;//show it
                timerarrival1.Start();// start the 5 second timer for gif
                wplayer.URL = "elev.mp3";// the music is playing, the file is in the same folder as the .exe
                wplayer.controls.play();
                Remove_Call1(call1);// after the 1st call is made I remove it by calling this method

                // On the floor of the call buttons are re-enabled by answering the call
                if (elevfl1 == 0)
                {
                    elevator1f0.Enabled = true;
                    floor0bu.Enabled = true;
                }
                else if (elevfl1 == 1)
                {
                    elevator1f1.Enabled = true;
                    floor1bd.Enabled = true;
                    floor1bu.Enabled = true;
                }
                else if (elevfl1 == 2)
                {
                    elevator1f2.Enabled = true;
                    floor2bd.Enabled = true;
                    floor2bu.Enabled = true;
                }
                else if (elevfl1 == 3)
                {
                    elevator1f3.Enabled = true;
                    floor3bd.Enabled = true;
                    floor3bu.Enabled = true;
                }
                else if (elevfl1 == 4)
                {
                    elevator1f4.Enabled = true;
                    floor4bd.Enabled = true;
                }

                Circle1(call1);// this method adds 1 repeat, that is how long each call waits for
            }
            else// if the elevator is still not on the call floor
            {
                if (elevfl1 == 0)// if the elevator is on the 1st floor
                {
                    floor0l1.Load("http://i.imgur.com/NhZPt6q.png");//turn on light
                    if (call1[0, 0] > elevfl1)// with this if i will see if the elevator is going up or down
                    {// just goes up from the ground floor
                        elevfl1++;// so the elevator goes to the next floor
                    }
                }
                else if (elevfl1 == 1)
                {
                    floor1l1.Load("http://i.imgur.com/NhZPt6q.png");
                    if (call1[0, 0] > elevfl1)// if it goes up
                    {
                        elevfl1++;
                    }
                    else if (call1[0, 0] < elevfl1)
                    {
                        elevfl1--;// if it goes down
                    }
                }
                else if (elevfl1 == 2)
                {
                    floor2l1.Load("http://i.imgur.com/NhZPt6q.png");
                    if (call1[0, 0] > elevfl1)
                    {
                        elevfl1++;
                    }
                    else if (call1[0, 0] < elevfl1)
                    {
                        elevfl1--;
                    }
                }
                else if (elevfl1 == 3)
                {
                    floor3l1.Load("http://i.imgur.com/NhZPt6q.png");
                    if (call1[0, 0] > elevfl1)
                    {
                        elevfl1++;
                    }
                    else if (call1[0, 0] < elevfl1)
                    {
                        elevfl1--;
                    }
                }
                else if (elevfl1 == 4)
                {
                    floor4l1.Load("http://i.imgur.com/NhZPt6q.png");
                    if (call1[0, 0] < elevfl1)
                    {//from 4th floor only downwards
                        elevfl1--;
                    }
                }

                Circle1(call1);// this method adds 1 repeat, that is how long each call waits for

            }
            Check_Waits1(call1);// method to check if there is a call waiting for more than 10 repetitions

            printer();// shows the calls in order
            callchecker1.Start();// the Timer checks again if there is a 1st call

        }

        private void callchecker2_Tick(object sender, EventArgs e)// check if there is a 1st call for the 2nd elevator
        {
            if (call2[0, 0] != -1 && !timerarrival2.Enabled)
            {
                movement2.Start();// the corresponding timer for the 2nd elevator
               
            }
        }
      
        private void movement2_Tick(object sender, EventArgs e)
        {//same as movement1_Tick
            Check_Waits2(call2);

            Turn_Lights2(elevfl2);
            if (call2[0, 0] == elevfl2)
            {
                
                movement2.Stop();
                elevator2arrival.Location = new Point(loc2[call2[0, 0], 0], loc2[call2[0, 0], 1]);
                elevator2arrival.Visible = true;
                timerarrival2.Start();
                wplayer2.URL = "elev.mp3";
                wplayer2.controls.play();

                Remove_Call2(call2);
               
                if (elevfl2 == 0)
                {                   
                    elevator2f0.Enabled = true;
                    floor0bu.Enabled = true;                   
                }
                else if (elevfl2 == 1)
                {                    
                    elevator2f1.Enabled = true;
                    floor1bd.Enabled = true;
                    floor1bu.Enabled = true;                    
                }
                else if (elevfl2 == 2)
                {                    
                    elevator2f2.Enabled = true;
                    floor2bd.Enabled = true;
                    floor2bu.Enabled = true;                  
                }
                else if (elevfl2 == 3)
                {
                    elevator2f3.Enabled = true;
                    floor3bd.Enabled = true;
                    floor3bu.Enabled = true;                  
                }
                else if (elevfl2 == 4)
                {
                    elevator2f4.Enabled = true;
                    floor4bd.Enabled = true;                  
                }

                Circle2(call2);
            }
            else
            {
                if (elevfl2 == 0 && call2[0, 0] != elevfl2)
                {
                    
                    floor0l2.Load("http://i.imgur.com/NhZPt6q.png");
                    if (call2[0, 0] > elevfl2)
                    {
                        elevfl2++;
                    }
                }
                else if (elevfl2 == 1 && call2[0, 0] != elevfl2)
                {
                    
                    floor1l2.Load("http://i.imgur.com/NhZPt6q.png");
                    
                    if (call2[0, 0] > elevfl2)
                    {
                        elevfl2++;
                    }
                    else if (call2[0, 0] < elevfl2)
                    {
                        elevfl2--;
                    }
                }
                else if (elevfl2 == 2 && call2[0, 0] != elevfl2)
                {
                    
                    floor2l2.Load("http://i.imgur.com/NhZPt6q.png");
                    
                    if (call2[0, 0] > elevfl2)
                    {
                        elevfl2++;
                    }
                    else if (call2[0, 0] < elevfl2)
                    {
                        elevfl2--;
                    }
                }
                else if (elevfl2 == 3 && call2[0, 0] != elevfl2)
                {
                    
                    floor3l2.Load("http://i.imgur.com/NhZPt6q.png");
                   
                    if (call2[0, 0] > elevfl2)
                    {
                        elevfl2++;
                    }
                    else if (call2[0, 0] < elevfl2)
                    {
                        elevfl2--;
                    }
                }
                else if (elevfl2 == 4 && call2[0, 0] != elevfl2)
                {
                    
                   floor4l2.Load("http://i.imgur.com/NhZPt6q.png");
                    
                    if (call2[0, 0] < elevfl2)
                    {
                        elevfl2--;
                    }
                }
                
                Circle2(call1);

            }
            printer();
            callchecker2.Start();
        }

        #region Methods

        public void printer()// shows the calls in the order and floor where the elevator is located
        {
            string fl1, w1, fl2, w2;
            
                richTextBox1.Clear();
                richTextBox2.Clear();
          


            for (int i = 0; i <= 12; i++)//just printing the queues
            {
                
                    richTextBox1.AppendText((i + 1).ToString() + ", ");
                    fl1 = call1[i, 0].ToString();
                    w1 = call1[i, 1].ToString();
                    richTextBox2.AppendText((i + 1).ToString() + ", ");
                    fl2 = call2[i, 0].ToString();
                    w2 = call2[i, 1].ToString();

                    if (call1[i, 0] == -1)
                    {
                        fl1 = "no";
                        w1 = "no";
                    }
                    if (call2[i, 0] == -1)
                    {
                        fl2 = "no";
                        w2 = "no";
                    }

                    richTextBox1.AppendText(fl1 + " , " + "call " + w1 + " time waits" + Environment.NewLine);
                    richTextBox2.AppendText(fl2 + " , " + "call " + w2 + " time waits" + Environment.NewLine);
                

            }
            
                elevator1textbox.Text = "Floor : " + elevfl1;
                elevator2textbox.Text = "Floor : " + elevfl2;
           
        
    }



        public void Turn_Lights1(int f)// for the 1st elevator the methods numbered 1
        {

            //τα φώτα όλα off 
            floor0l1.Load("http://i.imgur.com/8Vws7O1.png");
            floor1l1.Load("http://i.imgur.com/8Vws7O1.png");
            floor2l1.Load("http://i.imgur.com/8Vws7O1.png");
            floor3l1.Load("http://i.imgur.com/8Vws7O1.png");
            floor3l1.Load("http://i.imgur.com/8Vws7O1.png");
            floor4l1.Load("http://i.imgur.com/8Vws7O1.png");

            if (f == 0)// turn on the floor light where the elevator is located
                floor0l1.Load("http://i.imgur.com/NhZPt6q.png");
            else if (f == 1)
                floor1l1.Load("http://i.imgur.com/NhZPt6q.png");
            else if (f == 2)
                floor2l1.Load("http://i.imgur.com/NhZPt6q.png");
            else if (f == 3)
                floor3l1.Load("http://i.imgur.com/NhZPt6q.png");
            else if (f == 4)
                floor4l1.Load("http://i.imgur.com/NhZPt6q.png");
       }

        
        public void Check_Waits1(int[,] call1)// method to check if there is a call waiting for 10 repetitions to answer
        // and rearrange the order that this expected call is answered 1st
        {
            int a, b, x, y;
            for (int i = 0; i <= flag1; i++)
                if (call1[i, 1] >= 10)// if there is a call waiting 10+ times
                {
                    x = call1[i, 0];// save this call information
                    y = call1[i, 1];
                    for (int j = i; i <= 11; i++)
                    {// delete it and move other calls that are farther away, 1 place closer
                        a = call1[j + 1, 0];
                        b = call1[j + 1, 1];
                        call1[j, 0] = a;
                        call1[j, 1] = b;
                    }
                    // the call that was waiting 12+ times I put it 1st and move all the other calls one place farther
                    for (int u = flag1 + 1; u >= 1; u--)
                    {
                        call1[u, 0] = call1[u - 1, 0];
                        call1[u, 1] = call1[u - 1, 1];
                    }
                    call1[0, 0] = x;
                    call1[0, 1] = y;

                    elevfl1 = floors1[call1[0, 0]];// set up the elevator floor the floor of this call to go there immediately
                     // however the elevator movement does not appear because it goes straight to the next iteration on this floor                         
                    for (int j = flag1; j >= 1; j--)
                    {// I need to get back in order since it may have floors that were farther back now, now they are closer and need to be answered first
                        if (Math.Abs(call1[j - 1, 0] - elevfl1) > Math.Abs(call1[j, 0] - elevfl1))
                        {// if the call floor is closer than the floor of the previous call
                          // swap position
                            a = call1[j - 1, 0];
                            call1[j - 1, 0] = call1[j, 0];
                            call1[j, 0] = a;
                            b = call1[j - 1, 1];
                            call1[j - 1, 1] = call1[j, 1];
                            call1[j, 1] = b;
                        }
                    }
                }
                
        }

        public void Remove_Call1(int[,] call1)// a method that deletes the 1st call after answering it
        {
            int a, b;
            for (int i = 0; i <= 11; i++)
            {
                a = call1[i + 1, 0];
                b = call1[i + 1, 1];
                call1[i, 0] = a;
                call1[i, 1] = b;
            }
            flag1--;

        }

        public void Circle1(int[,] call1)// a method that adds +1 to the 2nd dimension of the call1 matrix which counts how many repetitions it expects
        {
            for (int i = 0; i <= 12; i++)
            {
                if (call1[i, 0] != -1)
                    call1[i, 1]++;
            }
        }

        public void Turn_Lights2(int f)// corresponding methods for the 2nd elevator
        {
            
                
                floor0l2.Load("http://i.imgur.com/8Vws7O1.png");
                floor1l2.Load("http://i.imgur.com/8Vws7O1.png");
                floor2l2.Load("http://i.imgur.com/8Vws7O1.png");
                floor3l2.Load("http://i.imgur.com/8Vws7O1.png");
                floor3l2.Load("http://i.imgur.com/8Vws7O1.png");
                floor4l2.Load("http://i.imgur.com/8Vws7O1.png");

                if (f == 0)
                    floor0l2.Load("http://i.imgur.com/NhZPt6q.png");
                else if (f == 1)
                    floor1l2.Load("http://i.imgur.com/NhZPt6q.png");
                else if (f == 2)
                    floor2l2.Load("http://i.imgur.com/NhZPt6q.png");
                else if (f == 3)
                    floor3l2.Load("http://i.imgur.com/NhZPt6q.png");
                else if (f == 4)
                    floor4l2.Load("http://i.imgur.com/NhZPt6q.png");
           
        }

        public void Check_Waits2(int[,] call2)
        
        {
            int a, b, x, y;
            for (int i = 0; i <= flag2; i++)
            {
                if (call2[i, 1] >= 10)
                {
                    x = call2[i, 0];
                    y = call2[i, 1];
                    for (int j = i; i <= 11; i++)
                    {
                        a = call2[j + 1, 0];
                        b = call2[j + 1, 1];
                        call2[j, 0] = a;
                        call2[j, 1] = b;
                    }
                    
                    for (int u = flag1 + 1; u >= 1; u--)
                    {
                        call2[u, 0] = call2[u - 1, 0];
                        call2[u, 1] = call2[u - 1, 1];
                    }
                    call2[0, 0] = x;
                    call2[0, 1] = y;

                    elevfl2 = floors2[call2[0, 0]];
                                                   
                    for (int j = flag2; i >= 1; i--)
                    {
                        if (Math.Abs(call2[j - 1, 0] - elevfl2) > Math.Abs(call2[j, 0] - elevfl2))
                        {
                         
                            a = call2[j - 1, 0];
                            call2[j - 1, 0] = call2[j, 0];
                            call2[j, 0] = a;
                            b = call2[j - 1, 1];
                            call2[j - 1, 1] = call2[j, 1];
                            call2[j, 1] = b;
                        }
                    }
                }
            }
        }

        public void Remove_Call2(int[,] call2)
        {
            int a, b;
            for (int i = 0; i <= 11; i++)
            {
                a = call2[i + 1, 0];
                b = call2[i + 1, 1];
                call2[i, 0] = a;
                call2[i, 1] = b;
            }
            flag2--;

        }

        public void Circle2(int[,] call1)
        {
            for (int i = 0; i <= 12; i++)
            {
                if (call2[i, 0] != -1)
                    call2[i, 1]++;
            }
        }
        #endregion

        private void button12_Click(object sender, EventArgs e)//reset button
        {
            timerarrival1.Stop();
            timerarrival2.Stop();
            movement1.Stop();
            movement2.Stop();
            elevator1arrival.Visible = false;
            
            elevator1arrival.Location = new Point(loc1[0, 0], loc1[0, 1]);
            elevfl1 = floors1[0];
            elevator2arrival.Visible = false;
            
            elevator2arrival.Location = new Point(loc2[0, 0], loc2[0, 1]);
            elevfl2 = floors2[0];
            for (int i = 0; i <= flag1; i++)
            {
                call1[i, 0] = -1;
                call1[i, 1] = 0;
            }
            flag1 = -1;
            for (int i = 0; i <= flag2; i++)
            {
                call2[i, 0] = -1;
                call2[i, 1] = 0;
            }
            flag2 = -1;

            floor0l1.Load("http://i.imgur.com/NhZPt6q.png");
            floor1l1.Load("http://i.imgur.com/8Vws7O1.png");
            floor2l1.Load("http://i.imgur.com/8Vws7O1.png");
            floor3l1.Load("http://i.imgur.com/8Vws7O1.png");
            floor4l1.Load("http://i.imgur.com/8Vws7O1.png");
            floor0l2.Load("http://i.imgur.com/NhZPt6q.png");
            floor1l2.Load("http://i.imgur.com/8Vws7O1.png");
            floor2l2.Load("http://i.imgur.com/8Vws7O1.png");
            floor3l2.Load("http://i.imgur.com/8Vws7O1.png");
            floor4l2.Load("http://i.imgur.com/8Vws7O1.png");

            callchecker1.Start();
            callchecker2.Start();
        }

        private void button1_Click(object sender, EventArgs e)//print button
        {
            printer();
        }


    }
}
