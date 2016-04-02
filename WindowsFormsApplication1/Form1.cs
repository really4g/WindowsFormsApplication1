using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox3.Text=ADUnit.GetObjectDistinguishedName(objectClass.user, returnType.distinguishedName, textBox1.Text, textBox2.Text);
            //ADUnit.Enable("LDAP://CN=Гнеева Мира,OU=Отдел по работе с прайсами,OU=ПРОФ-Недвижимость,DC=prof,DC=loc");
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //
            int i=0;
            ArrayList alDomains = ADUnit.EnumerateDomains();
            do
                {
                    
                    TreeNode newNode = new TreeNode(alDomains[i] as String);
                    
                    DomainTree.Nodes.Add(newNode);
                    DomainTree.SelectedNode = newNode;
                    int j=0;

                    ArrayList OUs = ADUnit.EnumerateOU(ADUnit.FriendlyDomainToLdapDomain(newNode.Text));

                    do
                    {
                        string ouname = OUs[j] as string;
                        TreeNode OU = new TreeNode(ouname);
                        DomainTree.SelectedNode.Nodes.Add(OU);

                        j++;
                    } while (i <= OUs.Count - 1);
                    

                    i++;
                }
            while (i <= (alDomains.Count - 1));

        }
    }
}
