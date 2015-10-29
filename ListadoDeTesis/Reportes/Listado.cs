using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using ListadoDeTesis.Dto;
using ListadoDeTesis.Singletons;
using Microsoft.Office.Interop.Word;
using ScjnUtilities;

namespace ListadoDeTesis.Reportes
{
    public class Listado
    {
        private readonly ObservableCollection<Tesis> tesisImprimir;
        private readonly DateTime? fechaEnvio;

        Microsoft.Office.Interop.Word.Application oWord;
        Microsoft.Office.Interop.Word.Document oDoc;
        object oMissing = System.Reflection.Missing.Value;
        object oEndOfDoc = "\\endofdoc";

        //Microsoft.Office.Interop.Word.Table oTable;

        readonly string filepath = Path.GetTempFileName() + ".docx";

        public Listado(ObservableCollection<Tesis> tesisImprimir,DateTime? fechaEnvio)
        {
            this.tesisImprimir = tesisImprimir;
            this.fechaEnvio = fechaEnvio;
        }

        public void GeneraListado()
        {
            oWord = new Microsoft.Office.Interop.Word.Application();
            oDoc = oWord.Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oMissing);
            oDoc.PageSetup.Orientation = WdOrientation.wdOrientPortrait;


            try
            {
                //Insert a paragraph at the beginning of the document.
                Microsoft.Office.Interop.Word.Paragraph oPara1;
                oPara1 = oDoc.Content.Paragraphs.Add(ref oMissing);
                //oPara1.Range.ParagraphFormat.Space1;
                oPara1.Range.Text = "SUPREMA CORTE DE JUSTICIA DE LA NACIÓN";

                oPara1.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                oPara1.Range.Font.Bold = 1;
                oPara1.Range.Font.Size = 10;
                oPara1.Range.Font.Name = "Arial";
                oPara1.Format.SpaceAfter = 0;    //24 pt spacing after paragraph.
                oPara1.Range.InsertParagraphAfter();
                oPara1.Range.InsertParagraphAfter();
                oPara1.Range.Text = "COORDINACIÓN DE COMPILACIÓN Y ";
                oPara1.Range.InsertParagraphAfter();
                oPara1.Range.Text = "SISTEMATIZACIÓN DE TESIS";
                oPara1.Range.InsertParagraphAfter();
                oPara1.Range.InsertParagraphAfter();
                oPara1.Range.Text = "RELACIÓN DE TESIS PARA PUBLICAR EN EL SEMANARIO JUDICIAL DE LA FEDERACIÓN Y EN SU GACETA";
                oPara1.Range.InsertParagraphAfter();
                oPara1.Range.InsertParagraphAfter();
                oPara1.Range.Text = "(AL " + DateTimeUtilities.ToLongDateFormat(fechaEnvio).ToUpper() + ")";
                oPara1.Range.InsertParagraphAfter();
                oPara1.Range.Text = "TOTAL:   " + tesisImprimir.Count() + " TESIS";
                oPara1.Range.InsertParagraphAfter();
                oPara1.Range.InsertParagraphAfter();


                //Tesis de jurisprudencia del Pleno
                List<Tesis> imprime = this.GetPrintableSectionTesis(100, 10006, 1);
                this.PrintTable(imprime, 100, 10006, 1);

                //Tesis Aisladas del Pleno
                imprime = this.GetPrintableSectionTesis(100, 10006, 0);
                this.PrintTable(imprime, 100, 10006, 0);

                //Tesis de jurisprudencia de la Primera Sala
                imprime = this.GetPrintableSectionTesis(100, 10001, 1);
                this.PrintTable(imprime, 100, 10001, 1);

                //Tesis aisladas de la Primera Sala
                imprime = this.GetPrintableSectionTesis(100, 10001, 0);
                this.PrintTable(imprime, 100, 10001, 0);

                //Tesis de jurisprudencia de la Segunda Sala
                imprime = this.GetPrintableSectionTesis(100, 10002, 1);
                this.PrintTable(imprime, 100, 10002, 1);

                //Tesis aisladas de la Segunda Sala
                imprime = this.GetPrintableSectionTesis(100, 10002, 0);
                this.PrintTable(imprime, 100, 10002, 0);

                //Tesis de jurisprudencia de Plenos de Circuito
                imprime = this.GetPrintableSectionTesis(4, 0, 1);
                this.PrintTable(imprime, 4, 0, 1);

                //Tesis aisladas de Plenos de Circuito
                imprime = this.GetPrintableSectionTesis(4, 0, 0);
                this.PrintTable(imprime, 4, 0, 0);

                //Tesis de jurisprudencia de Tribunales Colegiados de Circuito
                imprime = this.GetPrintableSectionTesis(1, 0, 1);
                this.PrintTable(imprime, 1, 0, 1);

                //Tesis aisladas de Tribunales Colegiados de Circuito
                imprime = this.GetPrintableSectionTesis(1, 0, 0);
                this.PrintTable(imprime, 1, 0, 0);


                foreach (Section wordSection in oDoc.Sections)
                {
                    object pagealign = WdPageNumberAlignment.wdAlignPageNumberRight;
                    object firstpage = true;
                    wordSection.Footers[WdHeaderFooterIndex.wdHeaderFooterPrimary].PageNumbers.Add(ref pagealign, ref firstpage);
                }

                oWord.ActiveDocument.SaveAs(filepath);
                oWord.ActiveDocument.Saved = true;
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,Listado", "ListadoDeTesis");
            }
            finally
            {
                oWord.Visible = true;
                //oDoc.Close();

            }
        }

        /// <summary>
        /// Obtiene las tesis que se van a incluir en las tablas de cada una de las secciones
        /// </summary>
        /// <param name="idInstancia">Instancia que emite la tesis</param>
        /// <param name="idSubInstancia">Identificador del Organismo que emite la tesis</param>
        /// <param name="tipoTesis">Jurisprudencia o Tesis Aislada</param>
        /// <returns></returns>
        private List<Tesis> GetPrintableSectionTesis(int idInstancia, int idSubInstancia, int tipoTesis)
        {
            if (idInstancia == 100)
            {
                return (from n in tesisImprimir
                        where n.IdSubInstancia == idSubInstancia && n.Tatj == tipoTesis
                        orderby n.OrdenInstancia, n.Rubro
                        select n).ToList();
            }
            else
            {
                return (from n in tesisImprimir
                        where n.IdInstancia == idInstancia && n.Tatj == tipoTesis
                        orderby n.OrdenInstancia, n.Rubro
                        select n).ToList();
            }
        }

        private void PrintTable(List<Tesis> tesisPorImprimir,int idInstancia, int idSubInstancia, int tipoTesis)
        {
            if (tesisPorImprimir.Count > 0)
            {
                if (idSubInstancia == 10002)
                    this.AddTableSegundaSala(tesisPorImprimir, this.GetTableTitle(idInstancia, idSubInstancia), this.GetTipoTesisTitle(tipoTesis), idSubInstancia);
                else if (idInstancia == 1 || idInstancia == 4)
                    this.AddTableSegundaSala(tesisPorImprimir, this.GetTableTitle(idInstancia, idSubInstancia), this.GetTipoTesisTitle(tipoTesis), idInstancia);
                else
                    this.AddTableContent(tesisPorImprimir, this.GetTableTitle(idInstancia, idSubInstancia), this.GetTipoTesisTitle(tipoTesis));

                Microsoft.Office.Interop.Word.Paragraph oPara1;
                oPara1 = oDoc.Content.Paragraphs.Add(ref oMissing);

                oPara1.Range.InsertParagraphAfter();
                oPara1.Range.InsertParagraphAfter();
            }
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


        private void AddTableSegundaSala(List<Tesis> tesisAImprimir, string tabletitle, string tipoTesis, int idInstancia)
        {
            List<int> cellWidth;

            if (idInstancia == 10002)
                cellWidth = new List<int>() { 60, 80, 200, 100 };
            else
                cellWidth = new List<int>() { 60, 100, 80, 200 };


            int fila = 1;
            Range wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;

            Table oTable = oDoc.Tables.Add(wrdRng, tesisAImprimir.Count + 3, 4, ref oMissing, ref oMissing);
            //oTable.Rows[1].HeadingFormat = 1;
            oTable.Range.ParagraphFormat.SpaceAfter = 6;
            oTable.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
            oTable.Range.Font.Size = 9;
            oTable.Range.Font.Name = "Arial";
            oTable.Range.Font.Bold = 0;
            oTable.Borders.Enable = 1;

            oTable.Columns[1].SetWidth(cellWidth[0], WdRulerStyle.wdAdjustSameWidth);
            oTable.Columns[2].SetWidth(cellWidth[1], WdRulerStyle.wdAdjustSameWidth);
            oTable.Columns[3].SetWidth(cellWidth[2], WdRulerStyle.wdAdjustSameWidth);
            oTable.Columns[4].SetWidth(cellWidth[3], WdRulerStyle.wdAdjustSameWidth);

            oTable.Rows[fila].Cells[1].Merge(oTable.Rows[fila].Cells[4]);
            oTable.Rows[fila].Range.Text = tabletitle;

            fila++;

            oTable.Rows[fila].Cells[1].Merge(oTable.Rows[fila].Cells[4]);
            oTable.Rows[fila].Range.Text = tipoTesis;

            fila++;

            if (idInstancia == 10002)
            {
                

                oTable.Cell(fila, 1).Range.Text = "Consecutivo";

                oTable.Cell(fila, 2).Range.Text = "Núm. de identificación de la tesis";
                oTable.Cell(fila, 3).Range.Text = "Título y subtítulo";
                oTable.Cell(fila, 4).Range.Text = "Materia Sugerida por la Sala";

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
                    oTable.Cell(fila, 4).Range.Text = print.Rubro;
                    oTable.Cell(fila, 4).Range.Font.Size = 8;
                    oTable.Cell(fila, 4).Range.Font.ColorIndex = cellColor;

                    fila++;
                    consecutivo++;
                }
            }
            else
            {
              

                oTable.Cell(fila, 1).Range.Text = "Consecutivo";
                oTable.Cell(fila, 2).Range.Text = "Organismo";
                oTable.Cell(fila, 3).Range.Text = "Núm. de identificación de la tesis";
                oTable.Cell(fila, 4).Range.Text = "Título y subtítulo";

                fila++;
                int consecutivo = 1;

                

                foreach (Tesis print in tesisAImprimir)
                {
                    WdColorIndex cellColor = this.GetCellColor(print.IdColor);
                    oTable.Cell(fila, 1).Range.Text = consecutivo.ToString();
                    oTable.Cell(fila, 1).Range.Font.ColorIndex = cellColor;
                    oTable.Cell(fila, 2).Range.Text = (from n in OrganismosSingleton.Organismos
                                                       where n.IdOrganismo == print.IdSubInstancia
                                                       select n.Organismo).ToList()[0];
                    oTable.Cell(fila, 2).Range.Font.ColorIndex = cellColor;
                    oTable.Cell(fila, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
                    oTable.Cell(fila, 3).Range.Text = print.ClaveTesis;
                    oTable.Cell(fila, 3).Range.Font.ColorIndex = cellColor;
                    oTable.Cell(fila, 3).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    oTable.Cell(fila, 4).Range.Text = print.Rubro;
                    oTable.Cell(fila, 4).Range.Font.Size = 8;
                    oTable.Cell(fila, 4).Range.Font.ColorIndex = cellColor;
                    oTable.Cell(fila, 4).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;

                    fila++;
                    consecutivo++;
                }

            }
        }

        private WdColorIndex GetCellColor(int idColor)
        {
            if (idColor == 2)
                return WdColorIndex.wdRed;
            else if (idColor == 3)
                return WdColorIndex.wdBlue;
            else if (idColor == 4)
                return WdColorIndex.wdViolet;
            else if (idColor == 5)
                return WdColorIndex.wdDarkRed;
            else if (idColor == 6)
                return WdColorIndex.wdGreen;
            else
                return WdColorIndex.wdBlack;
        }

        private string GetTableTitle(int idInstancia, int idSubInstancia)
        {
            if (idInstancia == 100)
            {
                if (idSubInstancia == 10006)
                    return "PLENO DE LA SUPREMA CORTE DE JUSTICIA DE LA NACIÓN";
                else if (idSubInstancia == 10001)
                    return "PRIMERA SALA DE LA SUPREMA CORTE DE JUSTICIA DE LA NACIÓN";
                else if (idSubInstancia == 10002)
                    return "SEGUNDA SALA DE LA SUPREMA CORTE DE JUSTICIA DE LA NACIÓN";
                else
                    return "";
            }
            else if (idInstancia == 1)
            {
                return "TRIBUNALES COLEGIADOS DE CIRCUITO";
            }
            else
            {
                return "PLENOS DE CIRCUITO";
            }


        }

        private string GetTipoTesisTitle(int tipoTesis)
        {

            if (tipoTesis == 1)
                return "TESIS DE JURISPRUDENCIA";
            else
                return "TESIS AISLADAS"; 

        }
    }
}
