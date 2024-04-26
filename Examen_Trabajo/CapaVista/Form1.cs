using CapaDatos;
using CapaModelo;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaVista
{
    public partial class Form1 : Form
    {
        DProducto FunGenero = new DProducto();
        MProducto DatGenero = new MProducto();

        DCategoria dCategoria = new DCategoria();
        //MCategoria mCategoria = new MCategoria();

        public int CodLibro;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CargarProducto();
            CargarCategoria();
        }

        private void CargarCategoria()
        {
            try
            {
                comboBox1.DataSource = dCategoria.MostrarCategoria();

                comboBox1.DisplayMember = "Nombre";
                comboBox1.ValueMember = "IdCategoria";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CargarProducto()
        {
            int CantRegistros;
            try
            {
                dgvGenero.DataSource = FunGenero.MostrarProducto();

                dgvGenero.Columns[0].Visible = false;
                dgvGenero.Columns[5].Visible = false;

                CantRegistros = dgvGenero.RowCount;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                dgvGenero.ClearSelection();
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                bool valordecajas;
                valordecajas = true;

                foreach (Control Caja in groupBox2.Controls)
                {
                    if ((Caja) is TextBox)
                    {
                        if (String.IsNullOrEmpty(Caja.Text))
                        {
                            valordecajas = false;
                            break;
                        }
                    }
                }

                if (valordecajas)
                {
                    AgregarProducto();
                    CargarProducto();
                    //txtNGenero.Clear();
                }
                else
                {
                    Interaction.MsgBox("Ingrese un Género Literario", MsgBoxStyle.Information, "Mensaje del Sistema");
                    //txtNGenero.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AgregarProducto()
        {
            try
            {
                DatGenero.nombreProducto = txtProducto.Text;
                DatGenero.Descripcion = txtDescripcion.Text;
                DatGenero.precioProducto = Convert.ToDecimal(txtPrecio.Text);
                DatGenero.Proveedor = txtProveedor.Text;
                DatGenero.IdCategoria = Convert.ToInt32(comboBox1.SelectedValue.ToString());
                DatGenero.Stock = Convert.ToInt32(txtStock.Text);
                //DatGenero.fechaRegistro = Convert.ToDateTime(dtpRegistro.Value.Date);
                //DatGenero.fechaFabricacion = Convert.ToDateTime(dtpFabricacion.Value.Date);
                //DatGenero.fechaVencimiento = Convert.ToDateTime(dtpVencimiento.Value.Date);


                if (DProducto.AgregarProducto(DatGenero))
                    Interaction.MsgBox("El Registro fue Agregado", MsgBoxStyle.Information, "Mensaje del Sistema");
                else
                    Interaction.MsgBox("El Registro no fue Agregado", MsgBoxStyle.Exclamation, "Mensaje del Sistema");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnModificar_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    bool valordecajas;
            //    valordecajas = true;

            //    foreach (Control Caja in this.Controls)
            //    {
            //        if ((Caja) is TextBox)
            //        {
            //            if (String.IsNullOrEmpty(Caja.Text))
            //            {
            //                valordecajas = false;
            //                break;
            //            }
            //        }
            //    }

            //    if (valordecajas)
            //    {
            //        ModificarProducto();
            //        CargarProducto();
            //        //txtNGenero.Clear();
            //    }
            //    else
            //    {
            //        Interaction.MsgBox("Ingrese un Género Literario", MsgBoxStyle.Information, "Mensaje del Sistema");
            //        //txtNGenero.Focus();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

            //if (dgvGenero.SelectedRows.Count > 0)
            //{
            //    DataGridViewRow currentRow = dgvGenero.SelectedRows[0];
            //    int index = currentRow.Index;

            //    //txtIdConta.Text = dgvContador.Rows[index].Cells["Codigo"].Value.ToString();
            //    txtProducto.Text = dgvGenero.Rows[index].Cells["Producto"].Value.ToString();
            //    txtDescripcion.Text = dgvGenero.Rows[index].Cells["Descripcion"].Value.ToString();
            //    txtPrecio.Text = dgvGenero.Rows[index].Cells["Precio"].Value.ToString();
            //    txtProveedor.Text = dgvGenero.Rows[index].Cells["Proveedor"].Value.ToString();
            //    comboBox1.SelectedValue = dgvGenero.Rows[index].Cells["IdCategoria"].Value.ToString();
            //    //DatGenero.IdCategoria = Convert.ToInt32(comboBox1.SelectedValue.ToString());
            //    txtStock.Text = dgvGenero.Rows[index].Cells["Stock"].Value.ToString();

            //}
            //else
            //{
            //    MessageBox.Show("Selecciona un registro de la lista", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return;
            //}
        }

        //private void ModificarProducto()
        //{

        //}

    }
}
