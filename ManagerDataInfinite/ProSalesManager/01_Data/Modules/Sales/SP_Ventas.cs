﻿using ProSalesManager._01_Data.Connection;
using ProSalesManager._01_Data.Modules.Sales.Interfaces;
using ProSalesManager._03_Models.ModelsCrud;
using ProSalesManager._03_Models.ReservedModels;
using System.Data;
using System.Data.SqlClient;

namespace ProSalesManager._01_Data.Modules.Sales
{
    public class SP_Ventas : ISP_Ventas
    {
        public List<ComboBox> TipoVentaCB()
        {
            var oList = new List<ComboBox>();
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Select_TipoVenta_ComboBox", conexion);

                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oList.Add(new ComboBox()
                            {
                                id = Convert.ToInt32(dr["idTipoVenta"]),
                                descripcion = dr["descripcion"].ToString(),

                            });
                        }
                    }
                }
                return oList;
            }
            catch (Exception ex)
            {
                ErrorResult.ErrorMessage = ex.Message;
                return oList;
            }
        }
    }
}