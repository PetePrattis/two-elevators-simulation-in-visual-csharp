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

            //το elevator 1 ειναι το ασανσερ στα αριστερα, ενώ το 2ο το ασανσέρ στα δεξιά
            elevator2arrival.Visible = false;//το picturebox με την εικόνα gif του elevator 1
            elevator1arrival.Visible = false;//για το 2ο

            floor0l1.Load("http://i.imgur.com/NhZPt6q.png");//το φωτάκι του 1ου ασανσέρ στο ισόγειο είναι αναμμένο αφού απο εκεί ξεκινάνε
            floor1l1.Load("http://i.imgur.com/8Vws7O1.png");//τα υπόλοιπα φώτα είναι κλειστά
            floor2l1.Load("http://i.imgur.com/8Vws7O1.png");
            floor3l1.Load("http://i.imgur.com/8Vws7O1.png");
            floor4l1.Load("http://i.imgur.com/8Vws7O1.png");

            floor0l2.Load("http://i.imgur.com/NhZPt6q.png");//τα φώτα για το 2ο ασανσέρ
            floor1l2.Load("http://i.imgur.com/8Vws7O1.png");
            floor2l2.Load("http://i.imgur.com/8Vws7O1.png");
            floor3l2.Load("http://i.imgur.com/8Vws7O1.png");
            floor4l2.Load("http://i.imgur.com/8Vws7O1.png");
        }

        #region General
        public static int[] floors1 = { 0, 1, 2, 3, 4 };//μια μήτρα που θα σώζει τους ορόφους όπου βρίσκεται το ασανσέρ
        public static int[] floors2 = { 0, 1, 2, 3, 4 };
        int[,] loc1 = new int[5, 2];//η μήτρα με τις τοποθεσίες του Gif του ασανσέρ
        int[,] loc2 = new int[5, 2];
        public static int elevfl1;//η μεταβλητή που θα σώζει σε ποιόν όροφο βρίσκεται το ασανσερ
        public static int elevfl2;

        
        //αυτή η δισδιάστατη μήτρα θα σώζει αρχικά στην 1η διάσταση της τον όροφο της κλήσης
        //και στη 2η διάσταση της πόσες επαναλήψεις περιμένει η κάθε κλήση
        //μέγεθος 13 όσα και τα κουμπιά στην εφαρμογή
        public static int[,] call1 = new int[,] { { -1, 0 },//αρχικοποιώ με -1 τους ορόφους αφού δεν υπάρχουν κλήσεις
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
            //αρχικοποιώ τις τοποθεσίες όπου θα εμφανίζεται το gif του ασανσέρ
            //για το 1ο ασανσέρ
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

            //για το 2ο ασανσέρ
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

            //αρχικοποιούμε τα ασανσέρ στο ισόγειο
            elevator1arrival.Location = new Point(loc1[0, 0], loc1[0, 1]);
            elevfl1 = floors1[0];

            elevator2arrival.Location = new Point(loc2[0, 0], loc2[0, 1]);
            elevfl2 = floors2[0];

            callchecker1.Start();//ξεκινάω την διαδικασία ελέγχου για κλήση
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

        WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();//με την βοήθεια του windows media player θα παίζω τους 2 ήχους ανοίγματος της πόρτας του ασανσέρ
        WMPLib.WindowsMediaPlayer wplayer2 = new WMPLib.WindowsMediaPlayer();//δημιουργώ αντικείμενο του windows media player 2 φορές

        private void timerarrival1_Tick(object sender, EventArgs e)//σε αυτό το Timer θα εμφανίζω το Gif του 1ου ασανσέρ
        {//5 δευτερόλεπτα
            
            elevator1arrival.Visible = false;
            timerarrival1.Stop();
        }
        private void timerarrival2_Tick(object sender, EventArgs e)//του 2ου ασανσέρ
        {
            
            elevator2arrival.Visible = false;

            timerarrival2.Stop();
        }
   

        public static int flag1 = -1;//Pointers που δείχνουν πόσες κλήσεις υπάρχουν, για το 1ο ασανσέρ
        public static int flag2 = -1;//για το 2ο

        #region Buttons1
        //στο πάτημα των κουμπιών καλώ μέθοδο και περνάω μια τιμή, τον όροφο την κλήσης-κουμπιού
        //αυτλα είναι τα κουμπιά μέσα στο 1ο ασανσέρ
        private void elevator1f0_Click(object sender, EventArgs e)
        {
            Queue1(0);//καλώ την μέθοδο
            elevator1f0.Enabled = false;//δεν μπορείς να ξανακάνεις την ίδια κλήση μέχρι να απαντηθεί
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
        //αυτά είναι τα κουμπιά μέσα στο 2ο ασανσέρ
        //καλώ μια 2η μέθοδο και περνάω την τιμή του ορόφου
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

        //τα κουμπιά που βρίσκονται στους ορόφους
        //θα ελέγχω ποιό ασανσέρ είναι πιο κοντά στην κλήση ή αν είναι εξίσου κοντά το 1ο ασανσέρ πηγαίνει ούτως ή άλλως
        //και μετά καλώ την αντίστοιχη μέθοδο
        private void floor0bu_Click(object sender, EventArgs e)
        {
            if(elevfl1 == elevfl2 && elevfl1 == 0)//αν τα ασανσέρ βρίσκονται στον ίδιο όροφο
            {
                Queue1(0);
                floor0bu.Enabled = false;
            }
            else if(Math.Abs(elevfl1 - 0) <= Math.Abs(elevfl2 - 0))//αν το 1ο ασανσέρ είναι πιο κοντά ή αν και τα 2 ασανσέρ είναι εξίσου κοντά, το 1ο θα απαντήσει την κλήση
            {
                Queue1(0);
                floor0bu.Enabled = false;
            }
            else if(Math.Abs(elevfl1 - 0) > Math.Abs(elevfl2 - 0))//αν το 2ο ασανσέρ είναι πιο κοντά τότε αυτό θα απαντήσει την κλήση
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


        public void Queue1(int x)//αυτή είναι η μέθοδος η οποία φτιάχνει την σειρά των κλήσεων όταν δημιουργούνται
        {//το x είναι ο όροφος της κλήσης
            
            int a, b;

            call1[flag1 + 1, 0] = x;
            if (flag1 != -1)//αν δεν είναι η 1η κλήση
            {
                for (int i = flag1 + 1; i >= 1; i--)//έλεγχος για όσες κλήσης υπάρχουν
                {
                    if (Math.Abs(call1[i - 1, 0] - elevfl1) > Math.Abs(call1[i, 0] - elevfl1))
                    {//αν ο όροφος της νές κλήσης είναι πιο κοντά από τον όροφο της προηγούμενης κλήσης
                     //αυτές οι 2 κλήσεις ανταλλάζουν θέση
                        a = call1[i - 1, 0];
                        call1[i - 1, 0] = call1[i, 0];
                        call1[i, 0] = a;
                        b = call1[i - 1, 1];
                        call1[i - 1, 1] = call1[i, 1];
                        call1[i, 1] = b;
                    }
                    else if (call1[i, 0] == call1[i - 1, 0])
                    {//αν οι κλήσεις είναι απο τον ίδιο όροφο (άλλο κουμπί) μετράνε ως μια
                     //άρα τίποτα δεν συμβαίνει
                    }
                    else//αλλιώς στην επόμενη επανάληψη
                        break;
                }
                for (int i = 0; i <= flag1 + 1; i++)
                {//εδώ θα ελέγχω αν υπάρχουν κλήσεις στον ίδιο όροφο και θα διαγράφω τις έξτρα κλήσεις
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

            printer();//εμφανίζω τις κλήσεις με την σειρά
        }

        public void Queue2(int x)//αυτή είναι η ίδια μέθοδος με την Queue1 για την λίστα των κλήσεων του 2ου ασανσέρ
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

        private void callchecker1_Tick(object sender, EventArgs e)//timer που ελέγχει αν υπάρχει 1η κλήση στην λίστα με τις κλήσεις
        {
            if (call1[0, 0] != -1 && !timerarrival1.Enabled )
            {
                movement1.Start();//ο timer στον οποίο εκτελούνται οτι συμβαίνουν κατα την διάρκεια της κίνησης του ασανσέρ
            }
        }

        //το όνομα του ειναι movement1, δεν φαίνεται γιατί έκανα μετονομασία και δεν ανανεώθηκε
        private void movement_Tick(object sender, EventArgs e)//ο timer στον οποίο εκτελούνται οτι συμβαίνουν κατα την διάρκεια της κίνησης του ασανσέρ
        {//π.χ. η αλλαγή των ορόφων, το άναμμα των λαμπών κλπ

            Check_Waits1(call1);//μέθοδος που ελέγχει αν υπάρχει κλήση που περιμένει πάνω απο 10 επαναλήψεις

            Turn_Lights1(elevfl1);//μέθοδος που ανάβει τις λάμπες
            if (call1[0, 0] == elevfl1)//αν η 1η κλήση βρίσκεται στον ίδιο όροφο με το ασανσέρ
            {
                movement1.Stop();//σταματάει αυτή η διαδικασία
                elevator1arrival.Location = new Point(loc1[call1[0, 0], 0], loc1[call1[0, 0], 1]);//μετακίνηση του gif pictureBox
                elevator1arrival.Visible = true;//εμφάνισή του
                timerarrival1.Start();//εκκίνηση του timer των 5 δευετρολέπτων που είναι για το gif
                wplayer.URL = "elev.mp3";//παίζει η μουσική, το αρχείο βρίσκεται στον ίδιο φάκελο με το .exe
                wplayer.controls.play();
                Remove_Call1(call1);//αφού πραγματοποιηθεί η 1η κλήση την αφαιρώ καλώντας αυτην την μέθοδο

                //στον όροφο της κλήσης τα κουμπιά γίνονται ξανα enabled με την απάντηση της κλήσης
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

                Circle1(call1);//αυτή η μέθοδος προσθαίτει 1 επανάληψη, δηλαδή πόσο πολύ περιμένει η κάθε κλήση για να πραγματοποιηθεί

            }
            else//αν ακόμα δεν βρίσκεται το ασανσέρ στον όροφο της κλήσης
            {
                if (elevfl1 == 0)//αν το ασανσέρ βρίσκεται στον 1ο όροφο
                {
                    floor0l1.Load("http://i.imgur.com/NhZPt6q.png");//ανάβει το φως
                    if (call1[0, 0] > elevfl1)//με αυτό το if θα βλέπω αν το ασανσέρ είναι να πάει προς τα πάνω ή προς τα κάτω
                    {//από το ισόγειο μόνο πάνω πάει
                        elevfl1++;//έτσι το ασανσέρ πάει στον επόμενο όροφο
                    }
                }
                else if (elevfl1 == 1)
                {
                    floor1l1.Load("http://i.imgur.com/NhZPt6q.png");
                    if (call1[0, 0] > elevfl1)//αν πηγαίνει προς τα πάνω
                    {
                        elevfl1++;
                    }
                    else if (call1[0, 0] < elevfl1)
                    {
                        elevfl1--;//αν πηγαίνει προς τα κάτω
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
                    {//από τον 4ο όροφο μόνο κάτω πάει
                        elevfl1--;
                    }
                }

                Circle1(call1);//αυτή η μέθοδος προσθαίτει 1 επανάληψη, δηλαδή πόσο πολύ περιμένει η κάθε κλήση για να πραγματοποιηθεί

            }
            Check_Waits1(call1);//μέθοδος που ελέγχει αν υπάρχει κλήση που περιμένει πάνω απο 10 επαναλήψεις

            printer();//εμφανίζει τις κλήσεις με την σειρά
            callchecker1.Start();//ξεκινάει ξανά ο Timer ελέγχου αν υπάρζει μια 1η κλήση

        }

        private void callchecker2_Tick(object sender, EventArgs e)//έλεγχος αν υπάρζει μια 1η κλήση για το 2ο ασανσέρ
        {
            if (call2[0, 0] != -1 && !timerarrival2.Enabled)
            {
                movement2.Start();//ο αντίστοιχος timer για το 2ο ασανσέρ πραγματοποίησης της κίνησης
               
            }
        }
      
        private void movement2_Tick(object sender, EventArgs e)
        {//τα ίδια με το movement1_Tick
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

        public void printer()//εμφανίζει τις κλήσεις με την σειρά και τον όροφο στον οποίο βρίσκεται το ασανσέρ
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



        public void Turn_Lights1(int f)//για το 1ο ασανσέρ οι μέθοδοι με τον αριθμό 1 
        {

            //τα φώτα όλα off 
            floor0l1.Load("http://i.imgur.com/8Vws7O1.png");
            floor1l1.Load("http://i.imgur.com/8Vws7O1.png");
            floor2l1.Load("http://i.imgur.com/8Vws7O1.png");
            floor3l1.Load("http://i.imgur.com/8Vws7O1.png");
            floor3l1.Load("http://i.imgur.com/8Vws7O1.png");
            floor4l1.Load("http://i.imgur.com/8Vws7O1.png");

            if (f == 0)//ανάβω το φώς του ορόφου στον οποίο βρίσκεται τ οασανσέρ
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

        
        public void Check_Waits1(int[,] call1)//μέθοδος που ελέγχει αν υπάρχει κάποια κλήση που να περιμένει 10 επαναλήψεις για να απντηθεί
        //και αναδιοργανώνω την σειρά ώστε αυτη η κλήση που περιμένη να απντηθεί 1η
        {
            int a, b, x, y;
            for (int i = 0; i <= flag1; i++)
                if (call1[i, 1] >= 10)//αν υπάρχει κλήση που περιμένει 10+ φορές
                {
                    x = call1[i, 0];//σώζω τις πληροφορίες αυτής της κλήσης
                    y = call1[i, 1];
                    for (int j = i; i <= 11; i++)
                    {//την διαγράφω και μεταφέρω τις υπόλοιπες κλήσεις που είναι πιο μακρυα απο την απαντησή τους, 1 θέση πιο κοντά
                        a = call1[j + 1, 0];
                        b = call1[j + 1, 1];
                        call1[j, 0] = a;
                        call1[j, 1] = b;
                    }
                    //την κλήση που περίμενε 12+ φορές την βάζω 1η και μεταφέρω όλες τις υπολοιπες κλήσεις μια θέση πιο μακρυά
                    for (int u = flag1 + 1; u >= 1; u--)
                    {
                        call1[u, 0] = call1[u - 1, 0];
                        call1[u, 1] = call1[u - 1, 1];
                    }
                    call1[0, 0] = x;
                    call1[0, 1] = y;

                    elevfl1 = floors1[call1[0, 0]];//ορίζω ως τον όροφο του ασανσέρ τον όροφο της κλήσης αυτής ώστε να πάει εκεί αμέσως
                    //ωστόσο η κίνηση του ασανσέρ δεν φαίνεται διότι πάει κατευθείαν στην επόμενη επανάληψη σε αυτόν τον όροφο                          
                    for (int j = flag1; j >= 1; j--)
                    {//πρέπει να φτάξω ξανά την σειρά αφού μπορεί όροφοι που ήταν πιο μακρύα πριν, τώρα να είναι πιο κοντά και θα πρέπει να απντηθούν πρώτοι
                        if (Math.Abs(call1[j - 1, 0] - elevfl1) > Math.Abs(call1[j, 0] - elevfl1))
                        {//αν ο όροφος της κλήσης είναι πιο κοντά από τον όροφο της προηγούμενης κλήσης
                         //ανταλλάζουν θέση
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

        public void Remove_Call1(int[,] call1)//μια μέθοδος που διαγράφει την 1η κλήση μετά την απάντηση της
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

        public void Circle1(int[,] call1)//μια μέθοδος που προσθέτει +1 στην 2η διάσταση της μήτρας call1 το όποιο μετράει πόσες επαναλήψεις περιμένει
        {
            for (int i = 0; i <= 12; i++)
            {
                if (call1[i, 0] != -1)
                    call1[i, 1]++;
            }
        }

        public void Turn_Lights2(int f)//αντίστοιχες μέθοδοι για το 2ο ασανσέρ
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
