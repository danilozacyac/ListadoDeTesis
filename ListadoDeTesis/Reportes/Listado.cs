using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using ListadoDeTesis.Dto;
using Microsoft.Office.Interop.Word;

namespace ListadoDeTesis.Reportes
{
    public class Listado
    {
        private ObservableCollection<Tesis> tesisImprimir;

        Microsoft.Office.Interop.Word.Application oWord;
        Microsoft.Office.Interop.Word.Document oDoc;
        object oMissing = System.Reflection.Missing.Value;
        object oEndOfDoc = "\\endofdoc";

        //Microsoft.Office.Interop.Word.Table oTable;

        readonly string filepath = Path.GetTempFileName() + ".docx";

        public Listado(ObservableCollection<Tesis> tesisImprimir)
        {
            this.tesisImprimir = tesisImprimir;
        }

        public void GeneraListado()
        {
            oWord = new Microsoft.Office.Interop.Word.Application();
            oDoc = oWord.Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oMissing);
            oDoc.PageSetup.Orientation = WdOrientation.wdOrientPortrait;

            //Insert a paragraph at the beginning of the document.
            Microsoft.Office.Interop.Word.Paragraph oPara1;
            oPara1 = oDoc.Content.Paragraphs.Add(ref oMissing);
            //oPara1.Range.ParagraphFormat.Space1;
            oPara1.Range.Text = "SUPREMA CORTE DE JUSTICIA DE LA NACIÓN";
            oPara1.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
            oPara1.Range.Font.Bold = 1;
            oPara1.Range.Font.Size = 10;
            oPara1.Range.Font.Name = "Arial";
            oPara1.Format.SpaceAfter = 24;    //24 pt spacing after paragraph.
            oPara1.Range.InsertParagraphAfter();
            oPara1.Range.Text = "COORDINACIÓN DE COMPILACIÓN Y SISTEMATIZACIÓN";
            oPara1.Range.InsertParagraphAfter();
            oPara1.Range.Text = "DE TESIS";
            oPara1.Range.InsertParagraphAfter();
            oPara1.Range.Text = "RELACIÓN DE TESIS PARA PUBLICAR EN EL SEMANARIO JUDICIAL DE LA FEDERACIÓN Y SU GACETA";
            oPara1.Range.InsertParagraphAfter();
            oPara1.Range.Text = "(AL                        2015)";
            oPara1.Range.InsertParagraphAfter();
            oPara1.Range.Text = "TOTAL:   " +  tesisImprimir.Count() + " TESIS";
            oPara1.Range.InsertParagraphAfter();

            List<Tesis> imprime = (from n in tesisImprimir
                                   where n.IdInstancia == 100 && n.Tatj == 1
                                   orderby n.OrdenInstancia, n.Rubro
                                   select n).ToList();

            string tableTitle = "PLENO DE LA SUPREMA CORTE DE JUSTICIA DE LA NACIÓN";
            string tipoTesis = "TESIS DE JURISPRUDENCIA";
            this.AddTableContent(imprime, tableTitle, tipoTesis);

            oPara1.Range.InsertParagraphAfter(); 
            oPara1.Range.InsertParagraphAfter();

            imprime = (from n in tesisImprimir
                                   where n.IdInstancia == 100 && n.Tatj == 0
                                   orderby n.OrdenInstancia, n.Rubro
                                   select n).ToList();

            tipoTesis = "TESIS AISLADAS";
            this.AddTableContent(imprime, tableTitle, tipoTesis);


            //Microsoft.Office.Interop.Word.Range wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;

            //oTable = oDoc.Tables.Add(wrdRng, tesisImprimir.Count + 2, 3, ref oMissing, ref oMissing);
            ////oTable.Rows[1].HeadingFormat = 1;
            //oTable.Range.ParagraphFormat.SpaceAfter = 6;
            //oTable.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
            //oTable.Range.Font.Size = 9;
            //oTable.Range.Font.Bold = 0;
            //oTable.Borders.Enable = 1;

            //oTable.Columns[1].SetWidth(40, WdRulerStyle.wdAdjustSameWidth);
            //oTable.Columns[2].SetWidth(70, WdRulerStyle.wdAdjustSameWidth);
            //oTable.Columns[3].SetWidth(90, WdRulerStyle.wdAdjustSameWidth);

            //oTable.Rows[fila].Cells[1].Merge(oTable.Rows[fila].Cells[3]);

            //oTable.Cell(fila, 1).Range.Text = "Consecutivo";
            //oTable.Cell(fila, 2).Range.Text = "Número de identificación de la tesis";
            //oTable.Cell(fila, 3).Range.Text = "Título y subtítulo";

            //for (int x = 1; x < 4; x++)
            //{
            //    oTable.Cell(fila, x).Range.Font.Size = 10;
            //    oTable.Cell(fila, x).Range.Font.Bold = 1;
            //}


            //try
            //{
            //    ImprimeDocumento();

            //    foreach (Section wordSection in oDoc.Sections)
            //    {
            //        object pagealign = WdPageNumberAlignment.wdAlignPageNumberRight;
            //        object firstpage = true;
            //        wordSection.Footers[WdHeaderFooterIndex.wdHeaderFooterPrimary].PageNumbers.Add(ref pagealign, ref firstpage);
            //    }

            oWord.ActiveDocument.SaveAs(filepath);
            oWord.ActiveDocument.Saved = true;
            //}
            //catch (Exception ex)
            //{
            //    string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            //    MessageBox.Show("Error ({0}) : {1}" + ex.Source + ex.Message, methodName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    ErrorUtilities.SetNewErrorMessage(ex, methodName, 0);
            //}
            //finally
            //{
            oWord.Visible = true;
            //    oDoc.Close();

            //    Process.Start(filepath);
            //}
        }


        private void AddTableContent(List<Tesis> tesisAImprimir,string tabletitle, string tipoTesis)
        {
            int fila = 1;
            Range wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;

            Table oTable = oDoc.Tables.Add(wrdRng, tesisAImprimir.Count + 3, 3, ref oMissing, ref oMissing);
            //oTable.Rows[1].HeadingFormat = 1;
            oTable.Range.ParagraphFormat.SpaceAfter = 6;
            oTable.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
            oTable.Range.Font.Size = 9;
            oTable.Range.Font.Name = "Arial";
            oTable.Range.Font.Bold = 0;
            oTable.Borders.Enable = 1;

            oTable.Columns[1].SetWidth(60, WdRulerStyle.wdAdjustSameWidth);
            oTable.Columns[2].SetWidth(80, WdRulerStyle.wdAdjustSameWidth);
            oTable.Columns[3].SetWidth(300, WdRulerStyle.wdAdjustSameWidth);

            oTable.Rows[fila].Cells[1].Merge(oTable.Rows[fila].Cells[3]);
            oTable.Rows[fila].Range.Text = tabletitle;

            fila++;

            oTable.Rows[fila].Cells[1].Merge(oTable.Rows[fila].Cells[3]);
            oTable.Rows[fila].Range.Text = tipoTesis;

            fila++;

            oTable.Cell(fila, 1).Range.Text = "Consecutivo";
            
            oTable.Cell(fila, 2).Range.Text = "Núm. de identificación de la tesis";
            oTable.Cell(fila, 3).Range.Text = "Título y subtítulo";

            fila++;
            int consecutivo = 1;

            foreach (Tesis print in tesisAImprimir)
            {
                WdColorIndex cellColor = this.GetCellColor(print.IdColor);
                oTable.Cell(fila, 1).Range.Text = consecutivo.ToString();
                oTable.Cell(fila, 1).Range.Font.ColorIndex = cellColor;
                oTable.Cell(fila, 2).Range.Text = print.ClaveTesis;
                oTable.Cell(fila, 2).Range.Font.ColorIndex = cellColor;
                oTable.Cell(fila, 3).Range.Text = print.Rubro;
                oTable.Cell(fila, 3).Range.Font.ColorIndex = cellColor;
                oTable.Cell(fila, 3).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;

                fila++;
                consecutivo++;
            }
        }

        private WdColorIndex GetCellColor(int idColor)
        {
            if (idColor == 2)
            {
                return WdColorIndex.wdRed;
            }
            else if (idColor == 3)
            {
                return WdColorIndex.wdGreen;
            }
            else
                return WdColorIndex.wdBlack;
        }
    }
}
