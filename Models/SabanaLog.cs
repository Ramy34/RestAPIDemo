namespace RestAPIDemo.Models
{
    public class SabanaLog
    {

        public string IDLinea { get; set; }

        public string Evento { get; set; }

        public Int32 EventoAnterior { get; set; }

        public string EstatusProceso { get; set; }

        public string QuienPagaTiempo { get; set; }

        public string RolOrigen { get; set; }

        public string RolDestino { get; set; }

        public string Etapa { get; set; }

        public string Fase { get; set; }

        public DateTime Fecha { get; set; }

        public Int32 SecuenciaGeneral { get; set; }

        public Int32 SecuenciaDJ { get; set; }

        public Int32 SecuenciaBC { get; set; }

        public Int32 SecuenciaPLD { get; set; }

        public Int32 SecuenciaSPyPyTC { get; set; }

        public Int32 SecuenciaMA { get; set; }

        public Int32 SecuenciaRC { get; set; }

        public Int32 SecuenciaMC { get; set; }

        public string UsuarioCreator { get; set; }

    }
}
