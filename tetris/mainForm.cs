using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class mainForm : Form
    {
        private int[,] currentBrick;                     //当前的方块
        private int currentBrickNum;                     //当前方块序号
        private int currentDirection;                    //当前方块的方向
        private int currentX;                            //当前方块的横坐标
        private int currentY;                            //当前方块的纵坐标

        private int score;                               //分数

        private int nextBrickNum;                        //下一个方块的序号
        private int nextBrickDirection;                  //下一个方块的方向
        private int brickNum;                            //总的方块数目
        private int directionNum;                        //总的方向个数

        private Image myImage;                           //游戏面板背景
        private Random random;                           //生成随机数

        #region 方块定义
        /// <summary>
        /// 定义一个四维数组存放所有方块
        /// 定义方块int[i,j,y,x] 
        /// bricks:i为方块的序号,j为方块的方向,y为纵坐标(行数),x横坐标(列数)
        /// </summary>
        private int[, , ,] bricks = {
                                    //第一个方块
                                    {  
                                     {  
                                         {1,0,0,0},  
                                         {1,0,0,0},  
                                         {1,0,0,0},  
                                         {1,0,0,0}  
                                     },  
                                     {  
                                         {1,1,1,1},  
                                         {0,0,0,0},  
                                         {0,0,0,0},  
                                         {0,0,0,0}  
                                     },  
                                     {  
                                         {1,0,0,0},  
                                         {1,0,0,0},  
                                         {1,0,0,0},  
                                         {1,0,0,0}  
                                     },  
                                     {  
                                         {1,1,1,1},  
                                         {0,0,0,0},  
                                         {0,0,0,0},  
                                         {0,0,0,0}  
                                     }  
                                }, 
                                //第二个方块
                                {  
                                      {  
                                          {1,1,0,0},  
                                          {1,1,0,0},  
                                          {0,0,0,0},  
                                          {0,0,0,0}  
                                      },  
                                      {  
                                          {1,1,0,0},  
                                          {1,1,0,0},  
                                          {0,0,0,0},  
                                          {0,0,0,0}  
                                      },  
                                      {  
                                          {1,1,0,0},  
                                          {1,1,0,0},  
                                          {0,0,0,0},  
                                          {0,0,0,0}  
                                      },  
                                      {  
                                          {1,1,0,0},  
                                          {1,1,0,0},  
                                          {0,0,0,0},  
                                          {0,0,0,0}  
                                      }  
                                  }, 
                                  //第三个方块
                                  {  
                                      {  
                                          {1,0,0,0},  
                                          {1,1,0,0},  
                                          {0,1,0,0},  
                                          {0,0,0,0}  
                                      },  
                                      {  
                                          {0,1,1,0},  
                                          {1,1,0,0},  
                                          {0,0,0,0},  
                                          {0,0,0,0}  
                                      },  
                                      {  
                                          {1,0,0,0},  
                                          {1,1,0,0},  
                                          {0,1,0,0},  
                                          {0,0,0,0}  
                                      },  
                                      {  
                                          {0,1,1,0},  
                                          {1,1,0,0},  
                                          {0,0,0,0},  
                                          {0,0,0,0}  
                                      }  
                                  },
                                  //第四个方块
                                  {  
                                      {  
                                          {1,1,0,0},  
                                          {0,1,0,0},  
                                          {0,1,0,0},  
                                          {0,0,0,0}  
                                      },  
                                      {  
                                          {0,0,1,0},  
                                          {1,1,1,0},  
                                          {0,0,0,0},  
                                          {0,0,0,0}  
                                      },  
                                      {  
                                          {1,0,0,0},  
                                          {1,0,0,0},  
                                          {1,1,0,0},  
                                          {0,0,0,0}  
                                      },  
                                      {  
                                          {1,1,1,0},  
                                          {1,0,0,0},  
                                          {0,0,0,0},  
                                          {0,0,0,0}  
                                      }  
                                  }
                                 };

        #endregion

        #region 背景定义
        
        /// <summary>
        /// 定义背景数组
        /// 20行14列的二维数组
        /// </summary>
        private int[,] background ={  
                                    {0,0,0,0,0,0,0,0,0,0,0,0,0,0},  
                                    {0,0,0,0,0,0,0,0,0,0,0,0,0,0},  
                                    {0,0,0,0,0,0,0,0,0,0,0,0,0,0},  
                                    {0,0,0,0,0,0,0,0,0,0,0,0,0,0},  
                                    {0,0,0,0,0,0,0,0,0,0,0,0,0,0},  
                                    {0,0,0,0,0,0,0,0,0,0,0,0,0,0},  
                                    {0,0,0,0,0,0,0,0,0,0,0,0,0,0},  
                                    {0,0,0,0,0,0,0,0,0,0,0,0,0,0},  
                                    {0,0,0,0,0,0,0,0,0,0,0,0,0,0},  
                                    {0,0,0,0,0,0,0,0,0,0,0,0,0,0},  
                                    {0,0,0,0,0,0,0,0,0,0,0,0,0,0},  
                                    {0,0,0,0,0,0,0,0,0,0,0,0,0,0},  
                                    {0,0,0,0,0,0,0,0,0,0,0,0,0,0},  
                                    {0,0,0,0,0,0,0,0,0,0,0,0,0,0},  
                                    {0,0,0,0,0,0,0,0,0,0,0,0,0,0},  
                                    {0,0,0,0,0,0,0,0,0,0,0,0,0,0},  
                                    {0,0,0,0,0,0,0,0,0,0,0,0,0,0},  
                                    {0,0,0,0,0,0,0,0,0,0,0,0,0,0},  
                                    {0,0,0,0,0,0,0,0,0,0,0,0,0,0},  
                                    {0,0,0,0,0,0,0,0,0,0,0,0,0,0}  
                                };
        #endregion


        /// <summary>
        /// 构造函数
        /// </summary>
        public mainForm()
        {
            InitializeComponent();
            currentBrick = new int[4, 4];
            brickNum = 4;
            directionNum = 4;
            random = new Random();
        }

        /// <summary>
        /// 窗体默认加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            //初始化面板，得到面板对象作背景图片
            //this.panel1.ForeColor = Color.White;
            myImage = new Bitmap(panel1.Width, panel1.Height);
            
            //初始分数为0
            score = 0;
        }

        /// <summary>  
        /// 随机生成方块和状态  
        /// </summary>  
        private void BeginGame()
        {
            
            currentBrickNum = nextBrickNum;
            currentDirection = nextBrickDirection;

            //随机生成方块序号和方向序号(0-4)  
            //Next函数包含下界，不包含上界
            nextBrickNum = random.Next(0, brickNum);
            nextBrickDirection = random.Next(0, directionNum);

            //赋值给当前方块  
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    currentBrick[y, x] = bricks[currentBrickNum, currentDirection, y, x];
                }
            }

            //从(7,0)位置开始放砖块
            currentX = 7;
            currentY = 0;

            //开启计时器
            timer1.Start();
        }

        /// <summary>  
        ///  旋转方块  
        /// </summary>  
        private void RotateBrick()
        {

            int tempDirection = (currentDirection + 1) % 4;  //顺时针旋转方块90度后的方向

            int[,] tempTrick = new int[4, 4];   //临时方块

            //把旋转后的方块保存到临时方块中，再判断是否能旋转
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    tempTrick[y, x] = bricks[currentBrickNum, tempDirection, y, x];

                    //检查旋转后方块是否越界
                    if (tempTrick[y,x]==1 && (currentY + y > 19 || currentX+x>13))
                    {
                        return;
                    }
                    //检查旋转后的方块是否与现有方块冲突
                    if (tempTrick[y,x]==1 && background[currentY+y,currentX+x]==1)
                    {
                        return;
                    }
                }
            }

            //如果能旋转再把临时方块保存到当前方块currentTrick中,并且修改方向
            currentBrick = tempTrick;
            currentDirection = (currentDirection + 1) % 4;
        }

        /// <summary>  
        /// 下落方块  
        /// </summary>  
        private void DownBrick()
        {
            //判断是否可以下落
            if (CheckMoveDown())
            {
                //下落时，纵坐标加1
                currentY++;
            }
            else
            {
                //如果方块已经堆积到画布的上边界
                if (currentY == 0)
                {
                    //计时停止，游戏结束
                    timer1.Stop();
                    MessageBox.Show("游戏结束");
                    return;
                }

                //下落完成，修改背景数组
                for (int y = 0; y < 4; y++)
                {
                    for (int x = 0; x < 4; x++)
                    {
                        if (currentBrick[y, x] == 1)
                        {
                            background[currentY + y, currentX + x] = currentBrick[y, x];
                        }
                    }
                }

                ComputeScore();   //计算得分,计算背景数组，消除都有方块的行

                BeginGame();  //初始化方块
            }
            DrawExistBrick();       //绘制方块
            DrawNextBrick();
        }


        /// <summary>  
        /// 检测是否可以继续下落 
        /// </summary>  
        private bool CheckMoveDown()
        {
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    if (currentBrick[y, x] == 1)
                    {
                        if (currentY+ y + 1 >= 20) 
                        {
                            //方块落到底部，不能再往下落
                            return false;
                        }
                        if (background[y + currentY + 1, x + currentX] == 1)
                        {
                            //下方有方块挡住，不能再往下落
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>  
        /// 检测方块是否可以左移  
        /// </summary>  
        private bool CheckMoveLeft()
        {
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    if (currentBrick[y, x] == 1)
                    {
                        if (x + currentX - 1 < 0)       
                        {
                            //左移后方块越界
                            return false;
                        }
                        if (background[y + currentY, x + currentX - 1] == 1)  
                        {
                            //左移后与现有方块冲突
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>  
        /// 检测方块是否可以右移  
        /// </summary>  
        private bool CheckMoveRight()
        {
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    if (currentBrick[y, x] == 1)
                    {
                        if (x + currentX + 1 >= 14)
                        {
                            //右移后方块越界
                            return false;
                        }
                        if (background[y + currentY, x + currentX + 1] == 1)
                        {
                            //右移后与现有方块冲突
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 画出已经落下的方块和正在下落的方块
        /// </summary>
        private void DrawExistBrick()
        {
            //创建窗体画布
            Graphics g = Graphics.FromImage(myImage);

            //清除以前画的图形
            g.Clear(this.panel1.BackColor);

            //画出已经落下的方块
            for (int bgy = 0; bgy < 20; bgy++)
            {
                for (int bgx = 0; bgx < 14; bgx++)
                {
                    if (background[bgy, bgx] == 1)
                    {
                        //定义方块每一个小单元的边长为20
                        g.FillRectangle(new SolidBrush(Color.DimGray), bgx * 20, bgy * 20, 20, 20);
                        g.DrawRectangle(new Pen(Color.Black,1), bgx * 20, bgy * 20, 20, 20);
                    }
                }
            }

            //绘制当前正在下落的方块  
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    if (currentBrick[y, x] == 1)
                    {
                        //定义方块每一个小单元的边长为20
                        g.FillRectangle(new SolidBrush(Color.DimGray), (x + currentX) * 20, (y + currentY) * 20, 20, 20);
                        g.DrawRectangle(new Pen(Color.Gainsboro,1), (x + currentX) * 20, (y + currentY) * 20, 20, 20);
                    }
                }
            }

            //获取面板的画布
            Graphics gg = panel1.CreateGraphics();
            gg.DrawImage(myImage, 0, 0);
        }

        /// <summary>
        /// 画出下一个方块，预览的下一个方块
        /// </summary>
        private void DrawNextBrick()
        {
            Graphics g = this.panel2.CreateGraphics();
            g.Clear(this.panel2.BackColor); //清除之前的图形
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    //j为横坐标,i为纵坐标
                    if (bricks[nextBrickNum, nextBrickDirection, y, x] == 1)
                    {
                        g.FillRectangle(new SolidBrush(Color.DimGray), x * 20, y * 20, 20, 20);
                        g.DrawRectangle(new Pen(Color.Gainsboro,1), x * 20, y * 20, 20, 20);
                    }
                }
            }
        }

        /// <summary>
        /// 计算得分,消去方块
        /// </summary>
        private void ComputeScore()
        {
            for (int y = 19; y >=0; y--)
            {
                bool isFull = true;
                for (int x = 13; x >=0; x--)
                {
                    if (background[y, x] == 0)
                    {
                        //一行当中有某个单元没有方块，这一行不能消去
                        isFull = false;
                        break;
                    }
                }
                if (isFull)
                {
                    //增加积分  
                    score = score + 10;
                    for (int y1 = y; y1 > 0; y1--)  //y1>0,消到第二行，第一行肯定没有方块
                    {
                        //消去一行：
                        //把要消去的这一行的上面的所有方块向下移一行
                        for (int x1 = 0; x1 < 14; x1++)
                        {
                                background[y1, x1] = background[y1 - 1, x1];
                        }
                    }
                    y++;    //纵坐标为消去的这一行 继续判断
                    label1.Text = "游戏得分： "+score.ToString();
                }
            }
        }

        /// <summary>
        /// 计时器事件监听方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            DownBrick();   
        }

        /// <summary>
        /// 按钮1的事件监听方法
        /// 重新开始游戏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //初始化背景数组
            for (int y = 0; y < 20; y++)
            {
                for (int x = 0; x < 14; x++)
                {
                    background[y, x] = 0;
                }
            }
            timer1.Interval = 1000;

            //生成第一个方块的序号和方向
            nextBrickNum = random.Next(0, brickNum);
            nextBrickDirection = random.Next(0, directionNum);

            BeginGame();      
            DrawExistBrick();       //绘制方块
            DrawNextBrick();    //绘制下一个方块
            this.Focus();       //将窗口变成输入焦点
        }

        /// <summary>
        /// 按钮2的事件监听方法
        /// 暂停或开始游戏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "暂停游戏")
            {
                button2.Text = "开始游戏";
                timer1.Stop();
            }
            else
            {
                button2.Text = "暂停游戏";
                timer1.Start();
            }
        }

        /// <summary>
        /// 键盘按下事件监听器方法
        /// 控制方块
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(this.button2.Text.Equals("开始游戏"))
            {
                //暂停的时候不响应键盘
                return;
            }

            if (e.KeyCode == Keys.W)
            {
                //W键按下
                //旋转方块
                RotateBrick();
                DrawExistBrick();
            }
            else if (e.KeyCode == Keys.A)
            {
                //A键按下
                //方块往左边移动
                if (CheckMoveLeft())
                {
                    currentX--;
                }
                DrawExistBrick();
            }
            else if (e.KeyCode == Keys.D)
            {
                //D键按下
                //方块向右移动
                if (CheckMoveRight())
                {
                    currentX++;
                }
                DrawExistBrick();
            }
            else if (e.KeyCode == Keys.S)
            {
                //S键按下
                //方块加速下落
                this.timer1.Interval = 20;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S)
            {
                //松开S键时恢复time1的时间间隔为1秒
                this.timer1.Interval = 1000;
            }
        }

    }
}