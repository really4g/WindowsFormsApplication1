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

        private void FillNode(TreeNode Noda )
        {
            //
            //DomainTree.Nodes[i].Tag

            int j = 0;

            ArrayList OUs = ADUnit.EnumerateOU((Noda.Tag as ADNode).dn);

            do
            {
                //
                string ouname = OUs[j] as string;
                if (ADUnit.WhatAdObjectIs(ouname, objectCategory.OrgUnit) == true)
                {
                    ArrayList AdUnitAttr = ADUnit.GetUsedAttributes(ouname);
                    if (AdUnitAttr.IndexOf("name") >= 0)
                    {
                        string ouSmallName = ADUnit.GetADObjectProperty(ouname, "name");
                        TreeNode OU = new TreeNode(ouSmallName);
                        ADNode _adNode = new ADNode();
                        _adNode.ou = OUs[j] as string;
                        _adNode.dn = ADUnit.GetADObjectProperty(ouname, "distinguishedName");
                        _adNode.ObjCat = ADUnit.GetADObjectProperty(ouname, "objectCategory");
                        _adNode.Expanded = ((ADUnit.WhatAdObjectIs(ouname, objectCategory.User) == true) | (ADUnit.WhatAdObjectIs(ouname, objectCategory.Computer) == true) | (ADUnit.WhatAdObjectIs(ouname, objectCategory.SecurityGroup) == true) | (ADUnit.WhatAdObjectIs(ouname, objectCategory.SharedFolder) == true)); ;
                        _adNode.isContainer = ((ADUnit.WhatAdObjectIs(ouname, objectCategory.OrgUnit) == true) | (ADUnit.WhatAdObjectIs(ouname, objectCategory.Container) == true) | (ADUnit.WhatAdObjectIs(ouname, objectCategory.Domain) == true));
                        OU.Tag = _adNode;
                        //TreeNode OU = new TreeNode(ouname);
                        DomainTree.Nodes[NodeInd].Nodes.Add(OU);
                    };
                };
                j++;
            } while (j <= OUs.Count - 1);


        }


        private void button3_Click(object sender, EventArgs e)
        {
            //
            int i=0;
            Boolean AllFilled = false;
            ArrayList alDomains = ADUnit.EnumerateDomains();
            do
            {                   
                TreeNode newNode = new TreeNode(alDomains[i] as String);
                ADNode RootADNode = new ADNode();
                RootADNode.ou = "";
                RootADNode.dn = ADUnit.GetADObjectProperty(alDomains[i] as String, "distinguishedName");
                RootADNode.ObjCat = ADUnit.GetADObjectProperty(alDomains[i] as String, "objectCategory");
                RootADNode.Expanded = true;
                RootADNode.isContainer = true;
                newNode.Tag = RootADNode;
                DomainTree.Nodes.Add(newNode);
                DomainTree.SelectedNode = newNode;
                int j=0;

                ArrayList OUs = ADUnit.EnumerateOU(ADUnit.FriendlyDomainToLdapDomain(newNode.Text));

                do
                {
                    //
                    string ouname = OUs[j] as string;                            
                    if (ADUnit.WhatAdObjectIs(ouname, objectCategory.OrgUnit) == true)
                    {
                        ArrayList AdUnitAttr = ADUnit.GetUsedAttributes(ouname);
                        if (AdUnitAttr.IndexOf("name") >= 0)
                        {
                            string ouSmallName = ADUnit.GetADObjectProperty(ouname, "name");
                            TreeNode OU = new TreeNode(ouSmallName);
                            ADNode _adNode = new ADNode();
                            _adNode.ou = OUs[j] as string;
                            _adNode.dn = ADUnit.GetADObjectProperty(ouname, "distinguishedName");
                            _adNode.ObjCat = ADUnit.GetADObjectProperty(ouname, "objectCategory");
                            _adNode.Expanded = ((ADUnit.WhatAdObjectIs(ouname, objectCategory.User) == true) | (ADUnit.WhatAdObjectIs(ouname, objectCategory.Computer) == true) | (ADUnit.WhatAdObjectIs(ouname, objectCategory.SecurityGroup) == true) | (ADUnit.WhatAdObjectIs(ouname, objectCategory.SharedFolder) == true)); ;
                            _adNode.isContainer = ((ADUnit.WhatAdObjectIs(ouname, objectCategory.OrgUnit) == true) | (ADUnit.WhatAdObjectIs(ouname, objectCategory.Container) == true) | (ADUnit.WhatAdObjectIs(ouname, objectCategory.Domain) == true));
                            OU.Tag = _adNode;

                            //TreeNode OU = new TreeNode(ouname);
                            DomainTree.SelectedNode.Nodes.Add(OU);
                        };
                    };
                        j++;
                } while (j <= OUs.Count - 1);
                    

                    i++;
            } while (i <= (alDomains.Count - 1));
            this.Text = DomainTree.GetNodeCount(true).ToString("G");
            i = 0;
            do
            {
                //break;
                if (i > DomainTree.Nodes.Count - 1) { i = 0; };

                
                FillNode((DomainTree.Nodes[i].Tag as ADNode).dn, i);
                //
                (DomainTree.Nodes[i].Tag as ADNode).Expanded = true;

                /*
                if (i > DomainTree.GetNodeCount(false) - 1) { i = 0; };

                DomainTree.Nodes[i].Nodes.IndexOf
                if (i > 0) { MessageBox.Show(DomainTree.Nodes[i].Text); }
                if ((DomainTree.Nodes[i].Tag as ADNode).Expanded == false) {
                    //
                    FillNode((DomainTree.Nodes[i].Tag as ADNode).dn,i);
                    //
                    (DomainTree.Nodes[i].Tag as ADNode).Expanded = true;*/
            } else
                { AllFilled = (AllFilled & ((DomainTree.Nodes[0].Tag as ADNode).Expanded == true)); }

                i++;
            } while (AllFilled == false);
                    

        }

        private void DomainTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (DomainTree.SelectedNode != null)
            {
                uOuDn.Text = (DomainTree.SelectedNode.Tag as ADNode).ou;
                uDN.Text = (DomainTree.SelectedNode.Tag as ADNode).dn;
                nExp.Checked = (DomainTree.SelectedNode.Tag as ADNode).Expanded;
                nCont.Checked = (DomainTree.SelectedNode.Tag as ADNode).isContainer;
                this.Text = (DomainTree.SelectedNode.Index).ToString("G");

            };
        }

        private void DomainTree_DoubleClick(object sender, EventArgs e)
        {
            if (DomainTree.SelectedNode != null)
            {
                //uOuDn.Text = DomainTree.SelectedNode.Tag as string;
                ouType.Text = ADUnit.GetADObjectProperty((DomainTree.SelectedNode.Tag as ADNode).ou, "objectCategory");
                Boolean isTrue = ADUnit.WhatAdObjectIs((DomainTree.SelectedNode.Tag as ADNode).ou, objectCategory.OrgUnit);
                if (isTrue.Equals(true)) { MessageBox.Show(objectCategory.OrgUnit); };
            };
        }
    }
    public class ADNode
    {
        public string ou = "";
        public string dn = "";
        public Boolean Expanded = false;
        public Boolean isContainer = true;
        public string ObjCat = objectCategory.OrgUnit;
    };
}
