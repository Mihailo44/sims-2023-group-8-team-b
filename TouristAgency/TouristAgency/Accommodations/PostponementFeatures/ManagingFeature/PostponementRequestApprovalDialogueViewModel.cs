using GalaSoft.MvvmLight.Messaging;
using System.Linq;
using System.Windows;
using TouristAgency.Accommodations.PostponementFeatures.Domain;
using TouristAgency.Accommodations.ReservationFeatures.Domain;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Util;

namespace TouristAgency.Accommodations.PostponementFeatures.ManagingFeature
{
    public class PostponementRequestApprovalDialogueViewModel : ViewModelBase, ICloseable
    {
        private ReservationService _reservationService;
        private PostponementRequestService _postponementRequestService;
        private readonly Window _window;

        public PostponementRequest SelectedRequest { get; }
        public DelegateCommand CloseCmd { get; set; }
        public DelegateCommand ApproveCmd { get; set; }
        public DelegateCommand DeclineCmd { get; set; }

        public PostponementRequestApprovalDialogueViewModel(PostponementRequest postponementRequest)
        {
            _window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Name == "PostponementDialogue");
            SelectedRequest = postponementRequest;
            InstantiateServices();
            InstantiateCommands();
        }

        private void InstantiateServices()
        {
            _reservationService = new();
            _postponementRequestService = new();
        }

        private void InstantiateCommands()
        {
            CloseCmd = new DelegateCommand(param => CloseCmdExecute(), param => CanCloseCmdExecute());
            ApproveCmd = new DelegateCommand(param => ApproveCmdExecute(), param => CanApproveCmdExecute());
            DeclineCmd = new DelegateCommand(param => DeclineCmdExecute(), param => CanDeclineCmdExecute());
        }

        public bool CanCloseCmdExecute()
        {
            return true;
        }

        public void CloseCmdExecute()
        {
            _window.Close();
        }

        public bool CanApproveCmdExecute()
        {
            return true;
        }

        public void ApproveCmdExecute()
        {
            Reservation reservation = _reservationService.ReservationRepository.GetById(SelectedRequest.Reservation.Id);
            PostponementRequest request = _postponementRequestService.PostponementRequestRepository.GetById(SelectedRequest.Id);
            bool accommodationAvailability = _reservationService.IsReserved(reservation.AccommodationId, SelectedRequest.Start, SelectedRequest.End);

            if (!accommodationAvailability)
            {
                reservation.Start = SelectedRequest.Start;
                reservation.End = SelectedRequest.End;
                _reservationService.ReservationRepository.Update(reservation, reservation.Id);

                request.Status = PostponementRequestStatus.APPROVED;
                _postponementRequestService.PostponementRequestRepository.Update(request, request.Id);
                MessageBox.Show("Reservation has been postponed", "Postponement Dialogue", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Postponement is not possible", "Postponement Dialogue", MessageBoxButton.OK, MessageBoxImage.Information);
                request.Status = PostponementRequestStatus.DENIED;
                request.Comment = "Sorry, the accommodation is reserved in this timeframe";
                _postponementRequestService.PostponementRequestRepository.Update(request, request.Id);
            }
            _window.Close();
        }

        public bool CanDeclineCmdExecute()
        {
            return true;
        }

        public void DeclineCmdExecute()
        {
            SelectedRequest.Status = PostponementRequestStatus.DENIED;
            _postponementRequestService.PostponementRequestRepository.Update(SelectedRequest, SelectedRequest.Id);
            Messenger.Default.Send(new SwitchViewModelMessage(new PostponementRequestCommentDialogueViewModel(SelectedRequest, _window)));
        }
    }
}
