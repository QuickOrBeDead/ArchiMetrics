﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ArchiMate.Core;

namespace ArchiMate
{
    public partial class MainForm : Form
    {
        private VisualStudioProjectGraph _graph;
        private VisualStudioProjectGraph _graphCached;

        private readonly IVisualStudioProjectRepository _projectRepository;
        private readonly IVisualStudioSolutionRepository _solutionRepository;

        public MainForm(IVisualStudioProjectRepository projectRepository, IVisualStudioSolutionRepository solutionRepository)
        {
            InitializeComponent();

            _projectRepository = projectRepository;
            _solutionRepository = solutionRepository;
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(textBox1.Text))
            {
                MessageBox.Show("Directory " + textBox1.Text + " doesn't exist!");
            }

            else
            {
                var projectsFileNames =
                    new List<string>(Directory.GetFiles(textBox1.Text, "*fsproj", SearchOption.AllDirectories));
                
                projectsFileNames.AddRange(
                    new List<string>(Directory.GetFiles(textBox1.Text, "*csproj", SearchOption.AllDirectories)));

                projectsFileNames.AddRange(
                    new List<string>(Directory.GetFiles(textBox1.Text, "*vbproj", SearchOption.AllDirectories)));

                _graph = new VisualStudioProjectGraph(_projectRepository.GetProjects(projectsFileNames));                

                BindGrids();
            }

        }

        void BindGrids()
        {
            var edges = _graph.Edges;

            if (!checkBox1.Checked)
            {
                edges = edges.Where(item => item.Source.Data.ProjectType != ".csproj").ToList();
                edges = edges.Where(item => item.Target.Data.ProjectType != ".csproj").ToList();
            }
            if (!checkBox2.Checked)
            {
                edges = edges.Where(item => item.Source.Data.ProjectType != ".fsproj").ToList();
                edges = edges.Where(item => item.Target.Data.ProjectType != ".fsproj").ToList();
            }
            if (!checkBox3.Checked)
            {
                edges = edges.Where(item => item.Source.Data.ProjectType != ".vbproj").ToList();
                edges = edges.Where(item => item.Target.Data.ProjectType != ".vbproj").ToList();
            }

            edges = edges.Where(item => Regex.IsMatch(item.Source.Name, textBox2.Text))
                    .Where(item => Regex.IsMatch(item.Target.Name, textBox3.Text)).ToList();

            _graphCached = new VisualStudioProjectGraph();

            foreach (Edge<VisualStudioProject> edge in edges)
            {
                _graphCached.AddEdge(edge.Source, edge.Target);
            }

            dataGridView2.DataSource = _graphCached.Edges.Select(item =>
                new { item.Id, Source = item.Source.Name, Target = item.Target.Name, TargetVertexType = item.Target.Data.ProjectType }).ToList();
            tabPage2.Text = "References (" + edges.Count + ")";

            var vertices = _graphCached.Vertices;

            dataGridView1.DataSource = vertices.OrderBy(item => item.Name).Select(item => new { Id = item.Id, Name = item.Name, ProjectType = item.Data.ProjectType, ProjectPath = item.Data.ProjectPath }).ToList();
            tabPage1.Text = "Projects (" + vertices.Count + ")";
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BindGrids();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _solutionRepository.CreateNewSolution(_graphCached, textBox1.Text + @"\Sample.Sln");
        }        
    }
}