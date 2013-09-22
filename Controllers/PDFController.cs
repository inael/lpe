using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using iTextSharp.text;
using iTextSharp.text.xml;
using iTextSharp.text.pdf;

namespace ViewWebMvc.Controllers
{
    /// <summary>
    /// Extends the MVC controller with functionlity for rendering PDF views.
    /// </summary>
    public class PDFController : Controller
    {
        /// <summary>
        /// Renders an action result to a string. This is done by creating a fake http context
        /// and response objects and have that response send the data to a string builder
        /// instead of the browser.
        /// </summary>
        /// <param name="result">The action result to be rendered to string.</param>
        /// <returns>The data rendered by the given action result.</returns>
        protected string RenderActionResultToString(ActionResult result)
        {
            // Create memory writer.
            var sb = new StringBuilder();
            var memWriter = new StringWriter(sb);

            // Create fake http context to render the view.
            var fakeResponse = new HttpResponse(memWriter);
            var fakeContext = new HttpContext(System.Web.HttpContext.Current.Request, fakeResponse);
            var fakeControllerContext = new ControllerContext(
                new HttpContextWrapper(fakeContext),
                this.ControllerContext.RouteData,
                this.ControllerContext.Controller);
            var oldContext = System.Web.HttpContext.Current;
            System.Web.HttpContext.Current = fakeContext;

            // Render the view.
            result.ExecuteResult(fakeControllerContext);

            // Restore data.
            System.Web.HttpContext.Current = oldContext;

            // Flush memory and return output.
            memWriter.Flush();
            return sb.ToString();
        }

        /// <summary>
        /// Returns a PDF action result. This method renders the view to a string then
        /// use that string to generate a PDF file. The generated PDF file is then
        /// returned to the browser as binary content. The view associated with this
        /// action should render an XML compatible with iTextSharp xml format.
        /// </summary>
        /// <param name="model">The model to send to the view.</param>
        /// <returns>The resulted BinaryContentResult.</returns>
        protected ActionResult ViewPdf(object model)
        {
            string imageFilePath = Server.MapPath("~/Content/imgs/timbrado.jpg");

            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageFilePath);

            // Create the iTextSharp document and margin left, right, top, bottom is defined.
            //Document doc = new Document(PageSize.A4, 80, 50, 30, 65);
            Document doc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            
            // Set the document to write to file.
            FileStream fileStream = new FileStream(Server.MapPath("~/my.pdf"), FileMode.Create);
            // Set the document to write to memory.
            MemoryStream memStream = new MemoryStream();
            
            PdfWriter writer = PdfWriter.GetInstance(doc, fileStream); //memStream);
            writer.CloseStream = false;
            
            //HeaderFooter header = new HeaderFooter(new Phrase("Texto do Cabeçalho"), false);
            //doc.Header = header;

            //Resize image depend upon your need
            //For give the size to image
            jpg.ScaleToFit(2481, 3447);

            //If you want to choose image as background then,
            jpg.Alignment = iTextSharp.text.Image.UNDERLYING;

            //If you want to give absolute/specified fix position to image.
            jpg.SetAbsolutePosition(0,15);

            jpg.ScalePercent(24f);

            doc.Open();

            string texto = @"estar neste mundo para cumprir uma missão inovadora.
É estimulada pelos obstáculos, e deposita fé tanto na sua capacidade de visão para mover montanhas, como no seu empenho na busca dos resultados; o impossível para os outros, para ela é uma questão de tempo.
Como se sente altamente segura de seus projetos, espera com todas as suas forças que suas ideias sejam aceitas e aplicadas pelos outros.
Por sua capacidade de resolução e persistência, pode facilmente conseguir que sua equipe trabalhe com o mesmo engajamento.
Dá grande valor à eficiência, tanto a própria quanto a dos outros.
Pessoas deste tipo são capazes de aproveitar ao máximo o momento presente, saboreando cada uma das múltiplas facetas.
Suas atitudes demonstram respeito à natureza.
 COMO SE MOTIVA
Ter atividades diferentes ao mesmo tempo.
Usar da originalidade e improvisar soluções para os problemas, visando o alcance de objetivos maiores.
Enfrentar desafios, dar sugestões, trabalhar com algo que necessite de sua habilidade de comunicação, persuasão, relacionamento com pessoas e de exercitar seu bom humor.
Ser valorizada por sua competência em resolver problemas com rapidez e eficiência.
 O QUE A DESAGRADA
Realizar atividades metódicas, uma de cada vez.
Trabalhar com atividades cujos procedimentos sejam rigidamente definidos.
Desenvolver tarefas sozinha, sem contato com equipe de trabalho.
Implementar projetos em que perceba ações antiecológicas.
 UTILIZANDO SEUS POTENCIAIS
Trabalhar em equipe, com possibilidade de liderar e estimular as pessoas a se engajarem em seus projetos.
Lidar com situações que ofereçam oportunidades de ação e exijam boa capacidade de adaptação.
Ser menos teimosa.
Pode atingir melhores resultados se contar em sua equipe com pessoas mais práticas e objetivas, que a ajudem a ponderar e refletir sobre as consequências de seus atos. Da mesma forma, acercar-se de colaboradores que desempenhem tarefas rotineiras, com regras, métodos e precisão, proporcionando-lhe maior organização e foco.";
            Font Myriad = new Font(FontFactory.GetFont(Server.MapPath("~/Content/Fonts/MyriadPro-Regular.otf"), 34, Font.NORMAL));
            texto = texto.Replace(Environment.NewLine, String.Empty).Replace("  ", String.Empty);
            
            Chunk beginning = new Chunk(texto, Myriad);
            Phrase p1 = new Phrase(beginning);

            // Heading
            Paragraph pHeading = new Paragraph(170, new Chunk("Relatório", Myriad));
            pHeading.Alignment = Element.ALIGN_CENTER;

            int pageNumber = -1;

            for (int i = 0; i < writer.PageNumber; i++)
            {
                if (pageNumber != writer.PageNumber)
                {
                    // Add image
                    doc.Add(jpg);
                }

                // Add something else
                doc.Add(pHeading);

                Paragraph paragraph = new Paragraph("2 this is the testing text for demonstrate the image is in background \n\n\n this is the testing text for demonstrate the image is in background");

                doc.Add(p1);
                doc.Add(paragraph);
            }

            

            // Render the view xml to a string, then parse that string into an XML dom.
            //string xmltext = this.RenderActionResultToString(this.View(model));
            //XmlDocument xmldoc = new XmlDocument();
            //xmldoc.InnerXml = xmltext.Trim();

            // Parse the XML into the iTextSharp document.
            //ITextHandler textHandler = new ITextHandler(doc);
            //textHandler.Parse(xmldoc);
            
            // Close and get the resulted binary data.
            doc.Close();
            
            // Memory Stream
            byte[] buf = new byte[memStream.Position];
            memStream.Position = 0;
            memStream.Read(buf, 0, buf.Length);

            // File Stream (salvar em arquivo)
            byte[] bufer = new byte[fileStream.Position];
            fileStream.Position = 0;
            fileStream.Read(bufer, 0, bufer.Length);

            fileStream.Close();

            // Send the binary data to the browser.
            return new BinaryContentResult(buf, "application/pdf");
        }
    }
}
