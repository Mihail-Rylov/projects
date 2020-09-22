using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }
        private int k = 0;
        private int iskznach;
        private string str1 = null;
        private string str2 = null;
        private string str3 = null;
        private string str4 = null;
        private string s1 = null;
        private string s2 = null;
        private string s3 = null;
        private string s4 = null;
        private string o1 = "В глубину CLR:";
        private string o2 = "В глубину LCR:";
        private string o3 = "В глубину RCL:";
        private string o4 = "В ширину:";
        private BinaryTree Tree = new BinaryTree();
        private Random rand = new Random();
        
        private void button1_Click(object sender, EventArgs e)
        {
            
            checkBox1.Enabled = true;
            string[] words = textBox1.Text.Split(new char[] { ' ' });
            for (int i = 0; i < words.Length; i++)
                Tree.Add(Convert.ToInt32(words[i]));
            Tree.PrintTree(listBox1);
            ObhodTree();
            Kratko();
            button1.Enabled = false;
            button2.Enabled = false;
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            if (checkBox1.Checked == true)
            {
                Podrobno();
            }
            else
            {
                Kratko();
            }  
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            checkBox1.Enabled = true;
            int n = Convert.ToInt32(textBox2.Text);
            for (int i = 0; i < n; i++)
                Tree.Add(rand.Next(0, 21));
            Tree.PrintTree(listBox1);
            ObhodTree();
            Kratko();
            button1.Enabled = false;
            button2.Enabled = false;
            
        }
        private void Kratko()
        {
            listBox2.Items.Add(o1);
            listBox2.Items.Add(s1);
            listBox2.Items.Add(o2);
            listBox2.Items.Add(s2);
            listBox2.Items.Add(o3);
            listBox2.Items.Add(s3);
            listBox2.Items.Add(o4);
            listBox2.Items.Add(s4);
        }
        private void Podrobno()
        {
            listBox2.Items.Add(o1);
            listBox2.Items.Add(str1);
            listBox2.Items.Add(o2);
            listBox2.Items.Add(str2);
            listBox2.Items.Add(o3);
            listBox2.Items.Add(str3);
            listBox2.Items.Add(o4);
            listBox2.Items.Add(str4);
        }
        private void ObhodTree()
        {
            Tree.CLR(Tree.root, ref str1, ref s1, ref iskznach, ref k);
            k = 0;
            Tree.LCR(Tree.root, ref str2, ref s2);
            Tree.RCL(Tree.root, ref str3, ref s3);
            Tree.Across(Tree.root, ref str4, ref s4);
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //if (Tree.root.data == iskznach) k--;
            iskznach = Convert.ToInt32(textBox3.Text);
            k = 0;
            Tree.CLR(Tree.root, ref str1, ref s1, ref iskznach, ref k);
            label5.Text = k.ToString();
            if (k != 0)
                label7.Text = "искомых значений найдено: " + k;
            else
                label7.Text = "искомое значение не найдено";
        }
    }
    class TreeElement
    {
        public int data;
        public TreeElement left, right;

        public TreeElement(int d)
        {
            data = d;
        }
    }
    class BinaryTree
    {
        public TreeElement root;
        public int Length = 0;
        protected int lvl;

        private void Print(TreeElement link, ListBox list)
        {
            if (link.left != null)
            {
                lvl++;
                list.Items.Add('|' + new string('|', lvl) + link.left.data.ToString());
                Print(link.left,list);
            }
            if (link.right != null)
            {
                lvl++;
                list.Items.Add('|' + new string('|', lvl) + link.right.data.ToString());
                Print(link.right,list);
            }
            lvl--;
        }

        public void PrintTree(ListBox list)
        {
            if (root != null)
            {
               list.Items.Add('|' + root.data.ToString());
                Print(root,list);
            }
            else
                list.Items.Add("Дерево пусто");
            lvl = 0;
        }

        private void Add_rec(int d, TreeElement link)
        {
            if (d < link.data)
                if (link.left == null)
                {
                    link.left = new TreeElement(d);
                    Length++;
                }
                else
                    Add_rec(d, link.left);
            if (d >= link.data)
                if (link.right == null)
                {
                    link.right = new TreeElement(d);
                    Length++;
                }
                else
                    Add_rec(d, link.right);
        }

        public void Add(int d)
        {
            if (root == null)
            {
                root = new TreeElement(d);
                Length++;
            }
            else
                Add_rec(d, root);
        }

        // прямой обход (CLR - center, left, right)
        public void CLR(TreeElement node, ref string str, ref string s, ref int iskznach,ref int k)
        {
            /*
             Аргументы метода:
             1. TreeNode node - текущий "элемент дерева" (ref  передача по ссылке)       
             2. ref string s - строка, в которой накапливается результат (ref - передача по ссылке)
            */
            
            if (node != null)
            {

                str += "    получили значение " + node.data.ToString() + Environment.NewLine;
                if (node.data == iskznach) k++;
                int temp = node.data;
                s += node.data.ToString() + " ";
                str += "    обходим левое поддерево" + Environment.NewLine;
                CLR(node.left, ref str, ref s, ref iskznach, ref k); // обойти левое поддерево
                //if (temp == iskznach) k--;
                str += "    обходим правое поддерево" + Environment.NewLine;
                CLR(node.right, ref str, ref s, ref iskznach, ref k); // обойти правое поддерево
            }
            else str += "    значение отсутствует - null" + Environment.NewLine;
        }
        // централизованный обход (LCR - left, center, right) 
        public void LCR(TreeElement node, ref string str, ref string s)
        {
            /*
             Аргументы метода:
             1. TreeNode node - текущий "элемент дерева" (ref - передача по ссылке)       
             2. ref string s - строка, в которой накапливается результат (ref - передача по ссылке)
            */
            if (node != null)
            {
                str += "    обходим левое поддерево" + Environment.NewLine;
                LCR(node.left, ref str, ref s); // обойти левое поддерево
                str += "    получили значение " + node.data.ToString() + Environment.NewLine;
                s += node.data.ToString() + " ";
                str += "    обходим правое поддерево" + Environment.NewLine;
                LCR(node.right, ref str, ref s); // обойти правое поддерево
            }
            else str += "    значение отсутствует - null" + Environment.NewLine;
        }
        // обратный обход (RCL -right, center, left)
        public void RCL(TreeElement node, ref string str, ref string s)
        {
            /*
             Аргументы метода:
             1. TreeNode node - текущий "элемент дерева" (ref  передача по ссылке)       
             2. ref string s - строка, в которой накапливается результат (ref - передача по ссылке)
            */
            if (node != null)
            {
                str += "    обходим правое поддерево" + Environment.NewLine;
                RCL(node.right, ref str, ref s); // обойти правое поддерево

                str += "    получили значение " + node.data.ToString() + Environment.NewLine;
                s += node.data.ToString() + " ";
                str += "    обходим левое поддерево" + Environment.NewLine;
                RCL(node.left, ref str, ref s); // обойти левое поддерево
            }
            else  str += "    значение отсутствует - null" + Environment.NewLine;
        }
        // обход дерева в ширину (итерационно, используется очередь)
        public void Across(TreeElement node, ref string str, ref string s)
        {
            /*
             Аргументы метода:
             1. TreeNode node - текущий "элемент дерева" (ref  передача по ссылке)       
             2. ref string s - строка, в которой накапливается результат (ref - передача по ссылке)
            */
            var queue = new Queue<TreeElement>(); // создать новую очередь
            str += "    заносим в очередь значение " + node.data.ToString() + Environment.NewLine;
            s += node.data.ToString() + " ";
            queue.Enqueue(node); // поместить в очередь первый уровень
            while (queue.Count != 0) // пока очередь не пуста
            {
                //если у текущей ветви есть листья, их тоже добавить в очередь
                if (queue.Peek().left != null)
                {
                    str += "    заносим в очередь значение " + queue.Peek().left.data.ToString() + " из левого поддерева" + Environment.NewLine;
                    s += queue.Peek().left.data.ToString() + " ";
                    queue.Enqueue(queue.Peek().left);
                }
                if (queue.Peek().right != null)
                {
                    str += "    заносим в очередь значение " + queue.Peek().right.data.ToString() + " из правого поддерева" + Environment.NewLine;
                    s += queue.Peek().right.data.ToString() + " ";
                    queue.Enqueue(queue.Peek().right);
                }
                //извлечь из очереди информационное поле последнего элемента
                str += "    извлекаем значение из очереди: " + queue.Peek().data.ToString() + Environment.NewLine;
                queue.Dequeue();
            }
        }
    }
}
