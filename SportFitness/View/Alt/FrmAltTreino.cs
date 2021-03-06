﻿using SportFitness.model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SportFitness.View.Alt
{
    public partial class FrmAltTreino : Form
    {
        private int idAluno, idTreinador, idObjetivo, idFichaTreino, idPlanoTreino, count;
        private ArrayList deletaExercicio;

        #region Carrega os dados do Treino
        public FrmAltTreino(int idPlanoTreino, int idFichaTreino, int idExercicio)
        {
            InitializeComponent();
            deletaExercicio = new ArrayList();
            this.idPlanoTreino = idPlanoTreino;
            this.idFichaTreino = idFichaTreino;
            comboGrupoMuscular.SelectedIndex = -1;
            comboExercicio.SelectedIndex = -1;
            FichaDetalhe ficha = new FichaDetalhe();
            count = ficha.getIdFichaDetalhe() + 1;

            PlanoTreino planoTreino = new PlanoTreino();

            foreach (PlanoTreino plano in planoTreino.selectArray("where id_planoTreino = "+idPlanoTreino))
            {
                dateInicio.Text = plano.DataInicio;
                maskedVezesSemana.Text = ""+plano.VezesSemana;
                idAluno = plano.IdAluno;
                idTreinador = plano.IdTreinador;
                idObjetivo = plano.IdObjetivo;
            }

            FichaTreino fichaTreino = new FichaTreino();

            foreach (FichaTreino fichaDet in fichaTreino.selectArray("where idPlanoTreino = "+idPlanoTreino))
            {
                //maskedNomeFicha.Text = ""+ficha.NomeFicha;
                //comboFichaAluno.SelectedItem = "" + fichaDet.NomeFicha;
                //textNumeroTreinosR.Text = "" + fichaDet.NumeroTreinosRealizados;
            }

            try
            {
                int n = Convert.ToInt16(comboFichaAluno.SelectedValue);
                if (n > 0)
                {
                    // MessageBox.Show("" + comboFichaAluno.SelectedValue);
                    FichaDetalhe fichaDet = new FichaDetalhe();
                    dataGridExercicios.DataSource = fichaDet.select(" where f.idFichaTreino=" + comboFichaAluno.SelectedValue);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {

            }

            //carregarExercicios(idFichaTreino);

            try
            {
                FichaTreino ficha1 = new FichaTreino();
                ArrayList array = ficha1.selectArray1(Convert.ToString(idPlanoTreino) + " order by nomeFicha");
                comboFichaAluno.DataSource = array;
                comboFichaAluno.DisplayMember = "nomeFicha";
                comboFichaAluno.ValueMember = "id";
            }
            catch
            {

            }
        }
        #endregion

        //#region Carrega os Exercicios na dataGrid
        ////Carrega os Exercicios
        //private void carregarExercicios(int idFichaTreino)
        //{
        //    dataGridExercicios.Rows.Clear();
        //    dataGridExerciciosList.Rows.Clear();
        //    FichaDetalhe fichaDetalhe = new FichaDetalhe();
            
        //    dataGridExerciciosList.DataSource = fichaDetalhe.altFicha("where fichaDetalhe.idFichaTreino = " + idFichaTreino);

        //    for (int i = 0; i < dataGridExerciciosList.RowCount; i++)
        //    {
        //        dataGridExercicios.Rows.Add(dataGridExerciciosList.Rows[i].Cells[0].Value.ToString(), dataGridExerciciosList.Rows[i].Cells[10].Value.ToString(), dataGridExerciciosList.Rows[i].Cells[8].Value.ToString(), dataGridExerciciosList.Rows[i].Cells[3].Value.ToString(), dataGridExerciciosList.Rows[i].Cells[4].Value.ToString(), dataGridExerciciosList.Rows[i].Cells[5].Value.ToString(), dataGridExerciciosList.Rows[i].Cells[2].Value.ToString());
                
        //    }
        //}
        //#endregion

        #region Carrega os grupos musculares no comboBox
        private void comboGrupoMuscular_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Exercicios exercicio = new Exercicios();
                ArrayList array = exercicio.selectArray("where idGrupoMuscular = " + comboGrupoMuscular.SelectedValue);
                comboExercicio.DataSource = array;
                comboExercicio.DisplayMember = "nome";
                comboExercicio.ValueMember = "id";
            }
            catch
            {

            }
        }
        #endregion

        #region Botão para Cancelar
        private void btCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Método para fechar a janela no ESC
        private void FrmAltTreino_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue.Equals(27)) //ESC
            {
                this.Close();
            }
        }
        #endregion

        #region Botão para Salvar
        private void btSalvar_Click(object sender, EventArgs e)
        {
            #region Validação dos componentes do cadastro
            if (comboAluno.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione um aluno.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboAluno.Focus();
                return;
            }

            if (comboTreinador.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione um Treinador.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboTreinador.Focus();
                return;
            }

            if (comboObjetivo.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione um objetivo.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboObjetivo.Focus();
                return;
            }

            if (dateInicio.Text.Trim().Length < 10)
            {
                MessageBox.Show("Digite uma data válida.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dateInicio.Focus();
                return;
            }

            if (maskedVezesSemana.Text.Trim().Length < 1)
            {
                MessageBox.Show("Digite uma quantidade de vezes na semana válido.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                maskedVezesSemana.Focus();
                return;
            }

            if (comboFichaAluno.SelectedIndex == -1)
            {
                MessageBox.Show("Digite um nome para ficha válido.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboFichaAluno.Focus();
                return;
            }

            if (dataGridExercicios.RowCount == 0)
            {
                MessageBox.Show("Insira algum exercício no treino.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboGrupoMuscular.Focus();
                return;
            }
            #endregion

            #region Alterar os dados
            PlanoTreino plano = new PlanoTreino();
            plano.Id = idPlanoTreino;
            plano.IdAluno = Convert.ToInt16(comboAluno.SelectedValue);
            plano.IdTreinador = Convert.ToInt16(comboTreinador.SelectedValue);
            plano.IdObjetivo = Convert.ToInt16(comboObjetivo.SelectedValue);
            plano.DataInicio = dateInicio.Text.Trim();
            plano.VezesSemana = Convert.ToInt16(maskedVezesSemana.Text.Trim());
            plano.Situacao = true;
            plano.update();

            FichaTreino ficha = new FichaTreino();
            ficha.Id = idFichaTreino;
            //ficha.NomeFicha = Convert.ToChar(comboFichaAluno.SelectedItem.ToString());
            ficha.Situacao = true;
            ficha.update();
            //ficha.insertIdPlanoTreino();

            FichaDetalhe detalhe = new FichaDetalhe();

            for(int i = 0;i < dataGridExercicios.RowCount; i++)
            {

                //MessageBox.Show("6- " + dataGridExercicios.Rows[i].Cells["ColumnIdExercicio"].Value.ToString());
                //MessageBox.Show("4- " + Convert.ToInt16(dataGridExercicios.Rows[i].Cells[""].Value.ToString()));
                //MessageBox.Show("5- " + Convert.ToInt16(dataGridExercicios.Rows[i].Cells[5].Value.ToString()));
                ////MessageBox.Show("3- " + Convert.ToInt16(dataGridExercicios.Rows[i].Cells[3].Value.ToString()));
                
                detalhe.IdExercicio = Convert.ToInt16(dataGridExercicios.Rows[i].Cells["ColumnIdExercicios"].Value.ToString());
                detalhe.Series = Convert.ToInt16(dataGridExercicios.Rows[i].Cells["ColumnSeries"].Value.ToString());
                detalhe.Repeticoes = Convert.ToInt16(dataGridExercicios.Rows[i].Cells["ColumnRepeticoes"].Value.ToString());
                detalhe.Carga = Convert.ToInt16(dataGridExercicios.Rows[i].Cells["ColumnCarga"].Value.ToString());
                detalhe.update();
            }

            MessageBox.Show("Treino Alterado com Sucesso.");
            #endregion

            #region Gravar o log
            //Geracao do log
            Logs logs = new Logs();
            string linha;

            using (StreamReader reader = new StreamReader("user.txt"))
            {
                linha = reader.ReadLine();
            }

            logs.IdUsuario = Convert.ToInt16(linha.ToString());
            logs.IdAcao = 16;
            logs.Data = DateTime.Today.ToString("dd/MM/yyyy");
            logs.Hora = DateTime.Now.ToString("HH:mm");
            logs.insert();
            #endregion

            this.Close();
        }

        private void FrmAltTreino_Load(object sender, EventArgs e)
        {
            groupBoxDados.Enabled = false;
        }

        private void btAdicionar_Click(object sender, EventArgs e)
        {
            //Verifica se componente esta vazio
            if (maskedVezesSemana.Text.Trim().Length < 1)
            {
                MessageBox.Show("Digite uma quantidade de vezes na semana válido.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                maskedVezesSemana.Focus();
                return;
            }

            //Verifica se componente esta vazio
            if (comboNomeFicha.SelectedIndex == -1)
            {
                MessageBox.Show("Digite um nome para ficha válido.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboNomeFicha.Focus();
                return;
            }

            #region Verifica se o nome da ficha de treino ja esta sendo utilizada
            //Verifica se o nome da ficha de treino ja esta sendo utilizada
            PlanoTreino planos = new PlanoTreino();
            DataTable dt = new DataTable();

            dt = planos.verificacaoNomeTreino(" where p.idAluno = " + comboAluno.SelectedValue + " and f.nomeFicha = '" + comboNomeFicha.SelectedItem.ToString() + "' and p.situacao = 1 and f.situacao = 1;");

            if (dt.Rows.Count >= 1)
            {
                MessageBox.Show("Este nome da ficha já esta sendo utilizado.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboNomeFicha.Focus();
                //comboNomeFicha.SelectedIndex = -1;
                return;
            }
            #endregion

            FichaTreino ficha = new FichaTreino();
            int id;
            id = ficha.getIdPlanoTreino();

            ficha.IdPlanoTreino = idPlanoTreino;
            ficha.NomeFicha = Convert.ToChar(comboNomeFicha.SelectedItem.ToString());
            ficha.NumeroTreinosRealizados = 0;
            ficha.Situacao = true;
            //MessageBox.Show("" + id);
            ficha.insert();

            comboNomeFicha.SelectedIndex = -1;
            carregarFichas();
        }
        #endregion

        private void carregarFichas()
        {
            #region Carregar as Fichas no comboBox
            try
            {
                FichaTreino ficha = new FichaTreino();
                ArrayList array = ficha.selectArray1(Convert.ToString(idPlanoTreino) + " order by nomeFicha");
                comboFichaAluno.DataSource = array;
                comboFichaAluno.DisplayMember = "nomeFicha";
                comboFichaAluno.ValueMember = "id";
            }
            catch
            {

            }
            #endregion
        }

        private void comboFichaAluno_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int n = Convert.ToInt16(comboFichaAluno.SelectedValue);
                if (n > 0)
                {
                    // MessageBox.Show("" + comboFichaAluno.SelectedValue);
                    FichaDetalhe fichaDet = new FichaDetalhe();
                    dataGridExercicios.DataSource = fichaDet.select(" where f.idFichaTreino=" + comboFichaAluno.SelectedValue);

                    FichaTreino fichaTreino = new FichaTreino();
                    foreach (FichaTreino ficha1 in fichaTreino.selectArray("where id_fichaTreino = " + comboFichaAluno.SelectedValue))
                    {
                        //maskedNomeFicha.Text = ""+ficha.NomeFicha;
                        textNumeroTreinosR.Text = "" + ficha1.NumeroTreinosRealizados;
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {

            }
        }

        #region Botão de Adicionar
        private void adicionar_Click(object sender, EventArgs e)
        {
            #region Validação dos componentes do cadastro
            if (comboGrupoMuscular.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione um grupo muscular.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboObjetivo.Focus();
                return;
            }

            if (comboExercicio.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione um exercício.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboObjetivo.Focus();
                return;
            }

            if (numericSeries.Value.Equals("") || numericSeries.Value == 0)
            {
                MessageBox.Show("Digite um número de série válido.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                numericSeries.Focus();
                return;
            }

            if (numericRepeticoes.Value.Equals("") || numericRepeticoes.Value == 0)
            {
                MessageBox.Show("Digite um número de repetições válido.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                numericRepeticoes.Focus();
                return;
            }

            if (numericCarga.Value.Equals(""))
            {
                MessageBox.Show("Digite um número de carga válido.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                numericCarga.Focus();
                return;
            }
            #endregion

            #region Salva o exercicio
            #region Verifica se o nome da ficha de treino ja esta sendo utilizada
            //Verifica se o nome da ficha de treino ja esta sendo utilizada
            FichaDetalhe fichaD = new FichaDetalhe();
            DataTable dt = new DataTable();

            dt = fichaD.verificaTreino(" where d.idExercicio = " + comboExercicio.SelectedValue + " and f.nomeFicha = '" + comboFichaAluno.Text.ToString() + "' and p.idAluno = " + comboAluno.SelectedValue + " and f.situacao = 1 and p.situacao = 1;");
            //MessageBox.Show("" + comboExercicio.SelectedValue + "" + comboFichaAluno.Text.ToString() + "" + comboAluno.SelectedValue);
            if (dt.Rows.Count >= 1)
            {
                MessageBox.Show("Esta ficha de treino já possui este exercício.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboGrupoMuscular.Focus();
                //comboNomeFicha.SelectedIndex = -1;
                return;
            }
            #endregion

            #region Adiciona na dataGrid 
            //dataGridExercicios.Rows.Add(count, comboGrupoMuscular.Text, comboExercicio.Text, numericSeries.Value, numericRepeticoes.Value, numericCarga.Value, comboExercicio.SelectedValue, comboFichaAluno.SelectedValue);
            //count++;
            FichaDetalhe detalhe = new FichaDetalhe();


            detalhe.IdFichaTreino = Convert.ToInt16(comboFichaAluno.SelectedValue);
            detalhe.IdExercicio = Convert.ToInt16(comboExercicio.SelectedValue);
            detalhe.Series = Convert.ToInt16(numericSeries.Value);
            detalhe.Repeticoes = Convert.ToInt16(numericRepeticoes.Value);
            detalhe.Carga = Convert.ToInt16(numericCarga.Value);
            detalhe.insert();
            #endregion

            #region Limpar os componentes
            comboGrupoMuscular.SelectedIndex = -1;
            comboExercicio.SelectedIndex = -1;
            numericSeries.Value = 0;
            numericRepeticoes.Value = 0;
            numericCarga.Value = 0;
            comboGrupoMuscular.Focus();
            #endregion

            try
            {
                int n = Convert.ToInt16(comboFichaAluno.SelectedValue);
                if (n > 0)
                {
                    // MessageBox.Show("" + comboFichaAluno.SelectedValue);
                    FichaDetalhe fichaDet = new FichaDetalhe();
                    dataGridExercicios.DataSource = fichaDet.select(" where f.idFichaTreino=" + comboFichaAluno.SelectedValue);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {

            }
            #endregion

            //#region Adiciona o exercicio na dataGrid
            //FichaDetalhe fichaDet = new FichaDetalhe();

            //dataGridExercicios.Rows.Add(count, comboGrupoMuscular.Text, comboExercicio.Text, numericSeries.Value, numericRepeticoes.Value, numericCarga.Value, comboExercicio.SelectedValue);
            //count++;
            //#endregion

            #region Limpa os componentes
            comboGrupoMuscular.SelectedIndex = -1;
            comboExercicio.SelectedIndex = -1;
            numericSeries.Value = 0;
            numericRepeticoes.Value = 0;
            numericCarga.Value = 0;
            comboGrupoMuscular.Focus();
            #endregion

        }
        #endregion

        #region Botão de Cancelar
        private void cancelar_Click(object sender, EventArgs e)
        {
            #region Limpa os componentes
            comboGrupoMuscular.SelectedIndex = -1;
            comboExercicio.SelectedIndex = -1;
            numericSeries.Value = 0;
            numericRepeticoes.Value = 0;
            numericCarga.Value = 0;
            comboGrupoMuscular.Focus();
            #endregion

        }
        #endregion

        #region Botão de Deletar
        private void deletar_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridExercicios.SelectedRows)
            {
                try
                {
                    FichaDetalhe fichaDet = new FichaDetalhe();
                    fichaDet.Id = Convert.ToInt16(dataGridExercicios.CurrentRow.Cells[0].Value);
                    fichaDet.delete();
                    dataGridExercicios.Rows.Remove(row);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        #endregion

        #region Método para pegar o ID do Grupo Muscular
        private string getIdGrupoMuscular(int id)
        {
            GrupoMuscular grupoMuscular = new GrupoMuscular();
            string nome = "";

            foreach(GrupoMuscular grupo in grupoMuscular.selectArray("where id_grupoMuscular = " + id)){
                nome = grupo.Nome;
            }

            return nome;
        }
        #endregion

        private void FrmAltTreino_Activated(object sender, EventArgs e)
        {
            #region Carrega os Alunos
            try
            {
                Alunos aluno = new Alunos();
                ArrayList array = aluno.selectArray("order by nome");
                comboAluno.DataSource = array;
                comboAluno.DisplayMember = "nome";
                comboAluno.ValueMember = "id";
            }
            catch
            {

            }
            #endregion

            #region Carrega os Treinadores
            try
            {
                Treinadores treinador = new Treinadores();
                ArrayList array = treinador.selectArray("order by nome");
                comboTreinador.DataSource = array;
                comboTreinador.DisplayMember = "nome";
                comboTreinador.ValueMember = "id";
            }
            catch
            {

            }
            #endregion

            #region Carrega os Objetivos
            try
            {
                Objetivo objetivo = new Objetivo();
                ArrayList array = objetivo.selectArray("order by nome");
                comboObjetivo.DataSource = array;
                comboObjetivo.DisplayMember = "nome";
                comboObjetivo.ValueMember = "id";
            }
            catch
            {

            }
            #endregion

            #region Carrega os Grupos Musculares
            try
            {
                GrupoMuscular grupo = new GrupoMuscular();
                ArrayList array = grupo.selectArray("order by nome");
                comboGrupoMuscular.DataSource = array;
                comboGrupoMuscular.DisplayMember = "nome";
                comboGrupoMuscular.ValueMember = "id";
            }
            catch
            {

            }
            #endregion

            comboAluno.SelectedValue = idAluno;
            comboTreinador.SelectedValue = idTreinador;
            comboObjetivo.SelectedValue = idObjetivo;
        }
    }
}